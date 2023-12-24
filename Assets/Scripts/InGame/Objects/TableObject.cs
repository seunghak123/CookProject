using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObject : BaseObject
{
    [SerializeField] protected TableObjectView tableObjectView;
    protected TableObjectViewDataClass tableObjectViewData;

    private BasicMaterialData mixedFood;
    private BasicMaterialData currentTableFood = new BasicMaterialData();
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = true;

        tableObjectView = GetComponent<TableObjectView>();
    }

    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        currentWork = false;
        //이건 오히려 param값이 있으면 실패
        if(targetAI.HandleObjectData==null || 
            targetAI.HandleObjectData.IsEmpty())
        {
            if(targetAI.HandleObjectData!=null&&targetAI.HandleObjectData.HasPlate())
            {
                currentTableFood.PushMaterial(1);   
            }
            targetAI.HandleObjectData = currentTableFood;
            currentTableFood = new BasicMaterialData();
            if(tableObjectViewData!=null)
            {
                tableObjectViewData.resultFoodId = currentTableFood.GetFoodId();
                UpdateUI();
            }
        }
        else if(targetAI.HandleObjectData.GetFoodResult().Count >= 1)
        {
            mixedFood = param;
            if (param != null)
            {
                currentWork = true;
                currentWorkRoutine = StartCoroutine(Working());
            }
        }
    }
    public override void SetToolData(JToolObjectData newToolData)
    {
        base.SetToolData(newToolData);

        if (tableObjectView != null)
        {
            tableObjectView.SetBaseSprite(newToolData);
        }
        UpdateUI();
    }
    protected override void UpdateUI()
    {
        if (tableObjectViewData == null)
        {
            tableObjectViewData = new TableObjectViewDataClass();
        }

        tableObjectViewData.resultFoodId = currentTableFood.GetFoodId();

        if (tableObjectView != null)
        {
            tableObjectView.Updated(tableObjectViewData);
        }
    }
    private void CreateNewFood()
    {
        List<int> foodResult = new List<int>(currentTableFood.GetFoodResult());
        foodResult.AddRange(mixedFood.GetFoodResult());

        if (IngameManager.currentManager.GetRecipeFoodResult(foodResult) == 0&&
            !currentTableFood.HasPlate())
        {
            mixedFood = null;
            return;
        }

        currentWorker.HandleObjectData = null;
        if(IngameManager.currentManager.GetRecipeFoodResult(foodResult) != 0)
        {
            currentTableFood.PushMaterialList(mixedFood.GetFoodResult());
        }

        if (currentTableFood.HasPlate())
        {
            currentTableFood.PushMaterial(1);
        }
        UpdateUI();
        mixedFood = null;
    }
    public override IEnumerator Working()
    {

        workEnd = true;

        if (IsWorkEnd())
        {
            CreateNewFood();
        }
        yield break; 
    }
}
