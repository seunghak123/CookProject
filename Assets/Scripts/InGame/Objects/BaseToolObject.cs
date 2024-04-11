using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseToolObject : BaseObject
{
    [SerializeField] protected bool holdCharacter = false;
    [SerializeField] protected bool isBlockCharacter = false;
    [SerializeField] public int objectDataID;
    protected bool workEnd = false;
    protected bool currentWork = false;
    protected BaseAI currentWorker = null;
    protected JToolObjectData toolData = null;
    protected Coroutine currentWorkRoutine = null;
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
    protected virtual void UpdateUI()
    {
    }
    public override bool GetIsHold()
    {
        return holdCharacter && IsWork();
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        currentWorker = targetAI; 
    }
    public override void ExitWork()
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
            return true;
        }

        return false;
    }
    public virtual void SetToolData(JToolObjectData newToolData)
    {
        toolData = newToolData;
    }
}