using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMaterialData 
{
    private List<int> materialLists = new List<int>();
    private int currentFoodId = 0;
    public bool IsEmpty()
    {
        if(materialLists.Count<=0)
        {
            return true;
        }
        return false;
    }
    public void SetFoodId(int foodId)
    {
        currentFoodId = foodId;
    }
    public int GetFoodId()
    {
        return currentFoodId;
    }
    public List<int> GetFoodResult()
    {
        return materialLists;
    }
    public void PushMaterial(int materialId)
    {
        //동일한 것이 있을때도 추가
        materialLists.Add(materialId);

        currentFoodId = IngameManager.currentManager.GetRecipeFoodResult(materialLists);
    }  
    public int GetFirstFoodId()
    {
        if(materialLists.Count>0)
        {
            return materialLists[0];
        }
        return 0;
    }
}
