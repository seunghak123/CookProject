using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleToolObject : ProgressedBaseObject
{
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;
    public override void InitObject()
    {
        base.InitObject();
        holdCharacter = false;
        isBlockCharacter = true;

        progressedView = GetComponent<MultipleToolObjectView>();

        workingAction = WorkingChange;
    }
    private void WorkingChange(ProgressedViewDataClass data)
    {
        if (progressedView != null)
        {
            progressedView.Updated(data);
        }
    }

    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        if(currentState>0)
        {
            //WorkEnd
            IsWorkEnd();
        }
        else
        {
            if (param != null)
            {
                preMaterial = param;
                currentWork = true;
                currentWorkRoutine = StartCoroutine(Working());
            }
        }
    }

    public override bool IsWorkEnd()
    {
        base.IsWorkEnd();

        //푸드 아이디에 따라 makedMaterial 값 세팅
        makedMaterial = new BasicMaterialData();
        switch(currentState)
        {
            case 1:
                
                break;
        }
        //가데이터
        makedMaterial.PushMaterial(3);

        currentWorker.HandleObjectData = makedMaterial;
        //앤 언제나 끝나있음
        return true;
    }
    //에셋번들-> Addressable
    //DOTS -> ECS 컴포넌트 기반 X 데이반 기반 시스템 - 메모리 적중률
}
