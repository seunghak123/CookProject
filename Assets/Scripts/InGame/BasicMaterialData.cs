using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMaterialData 
{
    private List<int> materialLists = new List<int>();
    
    public List<int> GetFoodResult()
    {
        return materialLists;
    }
    public void PushMaterial(int materialId)
    {
        //동일한 것이 있을때도 추가
        materialLists.Add(materialId);
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
