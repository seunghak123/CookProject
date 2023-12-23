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
        if (IsWorkEnd())
        {
            if (workingAI != null)
            {
                workingAI.HandleObjectData = makedMaterial;
            }
        }
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);

        workingAI = targetAI;


        //이건 오히려 param값이 있으면 실패

        if(param ==null)
        {
            StartCoroutine(Working());
        }
    }

    public override bool IsWorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;
            
            //푸드 아이디에 따라 makedMaterial 값 세팅
            makedMaterial = new BasicMaterialData();
            //가데이터
            makedMaterial.PushMaterial(toolData.OuputFood);

            return true;
        }

        return false;
    }
}
