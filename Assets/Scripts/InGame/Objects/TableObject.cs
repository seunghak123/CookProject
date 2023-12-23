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
        //�̰� ������ param���� ������ ����
        if(targetAI.HandleObjectData==null || 
            targetAI.HandleObjectData.IsEmpty())
        {
            targetAI.HandleObjectData = currentTableFood;
        }
        else if(targetAI.HandleObjectData.GetFoodResult().Count > 1)
        {

        }
        else
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

        if (tableObjectView != null)
        {
            tableObjectView.Updated(tableObjectViewData);
        }
    }
    private void CreateNewFood()
    {
        //���⼭ ���� �ʿ�
        List<int> foodResult = new List<int>(currentTableFood.GetFoodResult());
        foodResult.AddRange(mixedFood.GetFoodResult());

        if (IngameManager.currentManager.GetRecipeFoodResult(foodResult) == 0)
        {
            mixedFood = null;
            return;
        }

        currentWorker.HandleObjectData = null;

        currentTableFood.PushMaterial(mixedFood.GetFirstFoodId());

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
