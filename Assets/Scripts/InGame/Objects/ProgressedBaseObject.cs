using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressedViewDataClass : BaseViewDataClass
{
    public bool progressActive = false;
    public float currentPercent;
    public float currentIndex;
}
public class ProgressedBaseObject : BaseObject
{
    private ProgressedViewDataClass progressedViewData;
    [SerializeField]
    private ProgressObjectView progressedView;

    protected Action<ProgressedViewDataClass> workingAction;
    protected Action<ProgressedViewDataClass> timerWorkingAction;
    protected Action workingEndAction;

    //테스트 코드
    protected int currentState = 0;
    [SerializeField]
    private float[] workingTimeArrays = new float[] { 2, 10, 30 };
    //테스트 코드
    public override void InitObject()
    {
        base.InitObject();
        timerWorkingAction = ChangeProgressBarFront;
        workingEndAction = WorkingEnd;

        UpdateUI();
    }
    protected override void UpdateUI()
    {
        if(progressedViewData==null)
        {
            progressedViewData = new ProgressedViewDataClass();
        }

        if(progressedView!=null)
        {
            progressedView.Updated(progressedViewData);
        }
    }
    private void WorkingEnd()
    {
        ProgressedViewDataClass viewData = new ProgressedViewDataClass();

        progressedViewData = viewData;       
        UpdateUI();
    }
    private void ChangeProgressBarFront(ProgressedViewDataClass changeBar)
    {
        progressedViewData = changeBar;
        UpdateUI();
    }
    public override IEnumerator Working()
    {
        float timer = 0.0f;
        ProgressedViewDataClass viewData = new ProgressedViewDataClass();
        viewData.progressActive = true;
        for (int i = 0; i < workingTimeArrays.Length;)
        {
            if (timerWorkingAction != null)
            {
                viewData.currentPercent = timer / workingTimeArrays[i];
                timerWorkingAction(viewData);
            }
            if (timer> workingTimeArrays[i])
            {
                timer = 0;
                i++;
                viewData.currentIndex = currentState = i;
                if(workingAction!=null)
                {
                    workingAction(viewData);
                }
            }
            timer += Time.deltaTime;
            yield return WaitTimeManager.WaitForEndFrame();
        }

        workEnd = true;

        if (IsWorkEnd())
        {
            if(workingEndAction!=null)
            {
                workingEndAction();
            }
        }
    }
}
