using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObject : BaseObject
{
    private BasicMaterialData mixedFood;
    private BasicMaterialData currentTableFood = new BasicMaterialData();
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        //이건 오히려 param값이 있으면 실패
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
