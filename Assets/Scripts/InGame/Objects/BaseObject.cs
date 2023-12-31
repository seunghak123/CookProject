using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseObject : MonoBehaviour
{
    [SerializeField] protected bool holdCharacter = false;
    [SerializeField] protected bool isBlockCharacter = false;

    protected bool workEnd = false;
    protected bool currentWork = false;
    protected BaseAI currentWorker = null;
    protected JToolObjectData toolData = null;
    protected Coroutine currentWorkRoutine = null;
    private int objectDataID;
    public int OBJECT_ID
    {
        get { return objectDataID; }
        set {
            objectDataID = value;

            if (value < 0)
            {
                objectDataID = 0;
            }
        }
    }

    protected virtual bool IsCanInterAct(int interActObject)
    {
        //Type1번 완료
        List<JRecipeData> recipeData = JsonDataManager.Instance.GetOutputRecipeDatas(OBJECT_ID);

        if (recipeData!=null && recipeData.ConvertAll<int>(find => find.FoodName).Contains(interActObject))
        {
            return true;
        }
        return false;
    }
    protected virtual void Awake()
    {
        // 오브젝트 초기화
        InitObject();
    }
    protected virtual void UpdateUI()
    {
    }
    public virtual void InitObject() 
    {
    }
    public bool GetIsHold()
    {
        return holdCharacter && IsWork();
    }
    public virtual void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        currentWorker = targetAI; 
    }
    public virtual void ExitWork()
    {
        //나가졌을 때 처리하는 방법
    }
    public virtual IEnumerator Working()
    {
        yield break;
    }
    public bool IsWork()
    {
        return currentWork;
    }
    public virtual bool IsWorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;
            currentWorker.SetAnimationWithName("Idle");

            return true;
        }

        return false;
    }
    public virtual void SetToolData(JToolObjectData newToolData)
    {
        toolData = newToolData;
    }
}