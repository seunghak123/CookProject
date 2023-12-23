using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressedViewDataClass : BaseViewDataClass
{
    public bool progressActive = false;
    public float currentPercent;
    public int currentIndex;
    public List<int> foodInsertLists = new List<int>();
}
public class ProgressedBaseObject : BaseObject
{
    [SerializeField] protected ProgressObjectView progressedView;
    
    protected ProgressedViewDataClass progressedViewData;

    protected Action<ProgressedViewDataClass> workingAction;
    protected Action<ProgressedViewDataClass> timerWorkingAction;
    protected Action workingEndAction;
    protected int currentState = 0;
    protected float[] workingTimeArrays = new float[] { 2, 10, 30 };
    public override void InitObject()
    {
        base.InitObject();
        timerWorkingAction = ChangeProgressBarFront;
        workingEndAction = WorkingEnd;
        //View초기화
        progressedView = GetComponent<ProgressObjectView>();
    }
    public override void SetToolData(JToolObjectData newToolData)
    {
        base.SetToolData(newToolData);
        if(toolData!=null)
        {
            if (toolData.ToolTimer.Length <= 0)
            {
                workingTimeArrays = new float[] { 0 };
            }
            else
            {
                workingTimeArrays = toolData.ToolTimer;
            }
        }
        if (progressedView != null)
        {
            progressedView.SetBaseSprite(newToolData);
        }
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
        currentWork = true;
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
