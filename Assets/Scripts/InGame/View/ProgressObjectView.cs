using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressObjectView : BaseObjectView    
{
    [SerializeField] private Image progressFrontBar;
    public override void Updated(BaseViewDataClass data)
    {
        if (!IsInit())
        {
            return; 
        }
        if(!(data is ProgressedViewDataClass))
        {
            return;
        }
        ProgressedViewDataClass insertData = data as ProgressedViewDataClass;
        progressFrontBar.fillAmount = insertData.currentPercent;
    }

    protected override bool IsInit()
    {
        if(progressFrontBar==null)
        {
            return false;
        }
        return true;
    }
}