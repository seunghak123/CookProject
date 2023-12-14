﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCookerObject : ProgressedBaseObject
{
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;
    public override void InitObject()
    {
        base.InitObject();
        holdCharacter = false;
        isBlockCharacter = true;

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
                StartCoroutine(Working());
            }
        }
    }

    public override bool IsWorkEnd()
    {
        currentWork = false;

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
}
