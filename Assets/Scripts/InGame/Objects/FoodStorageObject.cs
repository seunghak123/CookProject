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
        if (workingAI != null)
        {
            workingAI.HandleObjectData = makedMaterial;
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
            currentWork = false;
            //접시일 떄
            if (toolData.OutputFood == 1)
            {
                if(workingAI.HandleObjectData==null)
                {
                    workingAI.HandleObjectData = new BasicMaterialData();
                }
                workingAI.HandleObjectData.PushMaterial(toolData.OutputFood);
            }
            else
            {
                makedMaterial = new BasicMaterialData();
                //푸드 아이디에 따라 makedMaterial 값 세팅
                //가데이터
                makedMaterial.PushMaterial(toolData.OutputFood);
            }
            
            return true;
        }

        return false;
    }
}
