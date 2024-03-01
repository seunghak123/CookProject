using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum E_MOVINGPLATFORM_TYPE
{
    E_DRIVE,
    E_LEVER,
    E_AUTO,
}
public class MovingPlatformGimmick : BaseGimmick
{
    [SerializeField] private E_MOVINGPLATFORM_TYPE movingType = E_MOVINGPLATFORM_TYPE.E_AUTO;
    [SerializeField] private float moveTime = 1.0f;
    [SerializeField] private int currentPosIndex = 0;
    //true : 정방향 false : 역방향
    [SerializeField] private bool currentDirect = true;
    [SerializeField] private List<Vector3> targetPosLists = new List<Vector3>();

    private List<Transform> contactTransforms = new List<Transform>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!contactTransforms.Contains(collision.transform))
        {
            contactTransforms.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (contactTransforms.Contains(collision.transform))
        {
            contactTransforms.Remove(collision.transform);
        }
    }

    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        if(targetAI!=null)
        {
            InterAct(targetAI);
        }

        //여기서 동작이 필요
    }
    private void MoveRelatedTransform(Vector3 moveDistance)
    {
        for(int i=0;i< contactTransforms.Count;i++ )
        {
            contactTransforms[i].position += moveDistance;
        }
    }
    public override void InitObject()
    {
        if(movingType == E_MOVINGPLATFORM_TYPE.E_AUTO)
        {
            InterAct(null);
        }
        //currentDirect 에 따라서 현재 위치 초기화 필요
        //true일 경우 0번 인덱스로 이동
        //false일 경우 마지막 인덱스로 이동
    }
    public override void ExitWork()
    {
        isStop = true;
        //나가졌을 때 처리하는 방법
    }
    public override bool GetIsHold()
    {
        if(movingType==E_MOVINGPLATFORM_TYPE.E_DRIVE)
        {
            return !isStop;
        }
        return false;
    }
    bool isStop = true;
    float saveTime = 0.0f;
    public async UniTask InterAct(BaseAI movedAI)
    {
        if (movingType == E_MOVINGPLATFORM_TYPE.E_DRIVE && !isStop)
        {
            isStop = true;
            return;
        }

        if (!isStop)
        {
            return;
        }
        if(targetPosLists.Count<2)
        {
            return;
        }
        float delayTime = 0.0f;

        Vector3 currentPos;

        int nextPosIndex = currentPosIndex + 1;
        if(!currentDirect)
        {
            nextPosIndex = currentPosIndex - 1;
        }

        switch (movingType)
        {
            case E_MOVINGPLATFORM_TYPE.E_DRIVE:
                isStop = false;

                while (!isStop)
                {
                    Vector3 moveDirect = movedAI.GetMoveDirect();
                    await UniTask.NextFrame(destroyCancellation.Token);
                    currentPos = Vector3.Lerp(targetPosLists[currentPosIndex], targetPosLists[nextPosIndex], delayTime / moveTime);
                    Vector3 direct = targetPosLists[nextPosIndex] - targetPosLists[currentPosIndex];
                    if (!IngameManager.currentManager.isPause)
                    {
                        if (direct.x > 0)
                        {
                            if (moveDirect.x > 0)
                            {
                                delayTime += Time.deltaTime;
                            }
                            else if (moveDirect.x < 0)
                            {
                                delayTime -= Time.deltaTime;
                            }
                        }
                        else
                        {
                            if (moveDirect.x < 0)
                            {
                                delayTime += Time.deltaTime;
                            }
                            else if (moveDirect.x > 0)
                            {
                                delayTime -= Time.deltaTime;
                            }
                        }
                    }
                    MoveRelatedTransform(currentPos - this.transform.position);
                    this.transform.position = currentPos;
                    if (Vector3.Distance(currentPos, targetPosLists[nextPosIndex]) < 0.01f)
                    {
                        currentPosIndex = nextPosIndex;
                        if (nextPosIndex == targetPosLists.Count - 1 || nextPosIndex == 0)
                        {
                            currentDirect = !currentDirect;
                        }
                        delayTime = 0.0f;
                        break;
                    }
                }
                break;
            case E_MOVINGPLATFORM_TYPE.E_LEVER:
                delayTime = saveTime;

                isStop = false;
                while (!isStop)
                {
                    if (!IngameManager.currentManager.isPause)
                    {
                        delayTime += Time.deltaTime;
                    }
                    await UniTask.NextFrame(destroyCancellation.Token);
                    currentPos = Vector3.Lerp(targetPosLists[currentPosIndex], targetPosLists[nextPosIndex], delayTime / moveTime);
                    MoveRelatedTransform(currentPos - this.transform.position);
                    this.transform.position = currentPos;
                    if (Vector3.Distance(currentPos, targetPosLists[nextPosIndex]) < 0.01f)
                    {
                        if (nextPosIndex == targetPosLists.Count - 1 || nextPosIndex == 0)
                        {
                            currentDirect = !currentDirect;
                        }
                        currentPosIndex = nextPosIndex;
                        delayTime = 0.0f;
                        break;
                    }
                }
                saveTime = delayTime;
                break;
            case E_MOVINGPLATFORM_TYPE.E_AUTO:
                while (true)
                {
                    if (!IngameManager.currentManager.isPause)
                    {
                        delayTime += Time.deltaTime;
                    }
                    await UniTask.NextFrame(destroyCancellation.Token);
                    currentPos = Vector3.Lerp(targetPosLists[currentPosIndex], targetPosLists[nextPosIndex], delayTime / moveTime);
                    Vector3 moveDistance = currentPos - this.transform.position;
                    this.transform.position = currentPos;
                    MoveRelatedTransform(moveDistance);
                    if (Vector3.Distance(currentPos, targetPosLists[nextPosIndex]) < 0.01f)
                    {
                        if (nextPosIndex == targetPosLists.Count - 1 || nextPosIndex == 0)
                        {
                            currentDirect = !currentDirect;
                        }
                        currentPosIndex = nextPosIndex;

                        delayTime = 0.0f;
                        if (currentDirect)
                        {
                            nextPosIndex = currentPosIndex + 1;
                        }
                        if (!currentDirect)
                        {
                            nextPosIndex = currentPosIndex - 1;
                        }
                    }
                }
        }

        isStop = true;
    }

#if UNITY_EDITOR
    public void AddTargetVector(Vector3 addVector)
    {
        targetPosLists.Add(addVector);
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(MovingPlatformGimmick))]
public class MovingPlatformButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MovingPlatformGimmick targetObject = (MovingPlatformGimmick)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("Add Target Vector"))
        {
            targetObject.AddTargetVector(targetObject.transform.position);
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(targetObject);
        }
    }
}
#endif

