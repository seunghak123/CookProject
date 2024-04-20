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
        int purposeFoodId = 0;
        if(param!=null)
        {
            if(param.GetFoodResult().Count>0)
            {
                preMaterial = param;
                purposeFoodId = IngameManager.currentManager.GetRecipeFoodResult(preMaterial.GetFoodResult());
            }
        }
        if (IsFail(param))
        {
            if(IsWork())
            {
                if(currentWorkRoutine!=null)
                {
                    StopCoroutine(currentWorkRoutine);
                }

                workEnd = true;
                if(IsWorkEnd())
                {
                    if (workingEndAction != null)
                    {
                        workingEndAction();
                    }
                    preMaterial = null;
                    StopCoroutine(currentWorkRoutine);
                }
            }
            else
            {
                preMaterial = null;
                return;
            }
        }
        else
        {
            ProgressedViewDataClass viewData = new ProgressedViewDataClass();
            viewData.purposeFoodId = purposeFoodId;
            progressedViewData = viewData;
            UpdateUI();

            currentWorker.HandleObjectData = null;
            currentWork = true;
            currentWorkRoutine = StartCoroutine(Working());
        }

    }

    private bool IsFail(BasicMaterialData param)
    {
        //매터리얼이 없고, 비어있고, 음식이 없으며, 상호작용이 불가능한 음식일 경우에 true
        if (param == null || param.IsEmpty() ||
            param.GetFirstFoodId() == 0 || !IsCanInterAct(param.GetFoodId()))
        {
            return true;
        }
        return false;
    }
    private void InitToolData()
    {
        currentWork = false;
        currentWorker = null;
        makedMaterial = null;
        preMaterial = null;
        currentState = 0;

        if(currentWorkRoutine!=null)
        {
            StopCoroutine(currentWorkRoutine);
        }
        currentWorkRoutine = null;

    }
    public override bool IsWorkEnd()
    {
        if (workEnd)
        {
            base.IsWorkEnd();

            switch(currentState)
            {
                case 0:
                    //아무것도 안했는데 뺼려는거
                    return false;
                case 1:
                    //요리성공
                    int aftetFoodId = IngameManager.currentManager.GetRecipeFoodResult(preMaterial.GetFoodResult());

                    //푸드 아이디에 따라 makedMaterial 값 세팅
                    makedMaterial = new BasicMaterialData();
                    //가데이터
                    makedMaterial.PushMaterial(aftetFoodId);
                    makedMaterial.SetFoodId(aftetFoodId);

                    currentWorker.HandleObjectData = makedMaterial;
                    InitToolData();
                    return true;
                case 2:
                    //요리가 탓을떄
                    InitToolData();
                    return true;
            }
        }

        return false;
    }

    //상호작용하면 물체를 들어올린다
}
