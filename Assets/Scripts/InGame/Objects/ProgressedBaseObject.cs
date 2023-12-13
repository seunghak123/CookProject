using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressedViewDataClass : BaseViewDataClass
{
    public float currentPercent;
    public float currentIndex;
}
public class ProgressedBaseObject : BaseObject
{
    private ProgressedViewDataClass progressedViewData;
    private ProgressObjectView progressedView;

    protected Action<ProgressedViewDataClass> workingAction;
    protected Action<ProgressedViewDataClass> timerWorkingAction;
    protected Action workingEndAction;

    //테스트 코드
    protected int currentState = 0;
    private float[] workingTimeArrays = new float[] { 2, 10, 30 };
    //테스트 코드
    public override void InitObject()
    {
        timerWorkingAction = ChangeProgressBarFront;
        workingEndAction = WorkingEnd;
    }
    protected override void UpdateUI()
    {
        progressedView.Updated(progressedViewData);
    }
    private void WorkingEnd()
    {

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
        for (int i = 0; i < workingTimeArrays.Length;)
        {
            if(timer> workingTimeArrays[i])
            {
                timer = 0;
                i++;
                viewData.currentIndex = currentState = i;
                if(workingAction!=null)
                {
                    workingAction(viewData);
                }
            }
            yield return WaitTimeManager.WaitForEndFrame();
            if(timerWorkingAction!=null)
            {
                viewData.currentPercent = timer / workingTimeArrays[i];
                timerWorkingAction(viewData);
            }
            timer += Time.deltaTime;
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
