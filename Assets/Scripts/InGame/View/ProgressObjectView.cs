using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressObjectView : BaseObjectView    
{
    [SerializeField] private Image progressFrontBar;
    [SerializeField] private Transform progressParent;
    public override void Updated(BaseViewDataClass data)
    {
        if(!IsChecked(data))
        {
            return;
        }
        ProgressedViewDataClass insertData = data as ProgressedViewDataClass;
        progressFrontBar.fillAmount = insertData.currentPercent;
        progressParent.gameObject.SetActive(insertData.progressActive);
        
        UpdateCurrentObjectState(insertData.currentIndex);
    }
    protected virtual void UpdateCurrentObjectState(int currentIndex)
    {

    }
    protected virtual bool IsChecked(BaseViewDataClass data)
    {
        if (!IsInit())
        {
            return false;
        }
        if (!(data is ProgressedViewDataClass))
        {
            return false;
        }
        return true;
    }

    protected override bool IsInit()
    {
        if(progressFrontBar==null)
        {
            return false;
        }
        if(progressParent==null)
        {
            return false;
        }
        return true;
    }
}