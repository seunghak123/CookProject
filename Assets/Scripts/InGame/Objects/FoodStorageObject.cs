using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoodStorageObject : ProgressedBaseObject
{
    //workingTime은 테이블에서 가져올것
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;
    private BaseAI workingAI = null;
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = true;
        workingEndAction = WorkingEnd;
        progressedView = GetComponent<ProgressObjectView>();
    }
    private void WorkingEnd()
    {
        workEnd = true;
        if (workingAI != null && (workingAI.HandleObjectData== null|| workingAI.HandleObjectData.GetFoodResult().Count <= 0) )
        {
            workingAI.HandleObjectData = makedMaterial;
        }
        else
        {
            if(toolData.ID.Equals(1015))
            {
                workingAI.HandleObjectData.PushMaterial(1);
            }
        }
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        workingAI = targetAI;

        //이건 오히려 param값이 있으면 실패
        if(param ==null || param.GetFoodResult().Count<=0 || toolData.ID.Equals(1015))
        {
            currentWork = true;
            currentWorkRoutine = StartCoroutine(Working());
        }
        else
        {
            currentWork = false;
        }
    }

    public override bool IsWorkEnd()
    {
        if (workEnd)
        {
            base.IsWorkEnd();
            currentWork = false;

            makedMaterial = new BasicMaterialData();
            makedMaterial.PutMaterial(toolData.OutputFood);

            return true;
        }

        return false;
    }
}
