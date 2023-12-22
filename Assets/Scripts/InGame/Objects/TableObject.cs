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
            if (param == null)
            {
                StartCoroutine(Working());
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
        currentTableFood.PushMaterial(mixedFood.GetFirstFoodId());
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
