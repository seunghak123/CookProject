using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seunghak.Common;
public class SingleToolObject : ProgressedBaseObject
{
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;

    public override void InitObject()
    {
        base.InitObject();
        holdCharacter = false;
        isBlockCharacter = false;
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI,param);

        preMaterial = param;

        if(IsFail())
        {
            return;
        }

        StartCoroutine(Working());
    }

    private bool IsFail()
    {
        //매터리얼이 없고, 비어있고, 음식이 없으며, 상호작용이 불가능한 음식일 경우에 true
        if (preMaterial == null || preMaterial.IsEmpty() || 
            preMaterial.GetFirstFoodId() == 0 || IsCanInterAct(preMaterial.GetFirstFoodId()))
        {
            return true;
        }
        return false;
    }

    public override bool IsWorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;

            int aftetFoodId = IngameManager.currentManager.GetRecipeFoodResult(preMaterial.GetFoodResult());

            //푸드 아이디에 따라 makedMaterial 값 세팅
            makedMaterial = new BasicMaterialData();
            //가데이터
            makedMaterial.PushMaterial(aftetFoodId);
            makedMaterial.SetFoodId(aftetFoodId);

            currentWorker.HandleObjectData = makedMaterial;
            return true;
        }

        return false;
    }

    //상호작용하면 물체를 들어올린다
}
