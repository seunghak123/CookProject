using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIngameUI : BaseIngameUI
{
    protected List<RecipeObject> reciptObjectLists = new List<RecipeObject>();
    public override void CreateRecipe(int recipeId)
    {
        GameObject spawnedObject = GameResourceManager.Instance.SpawnObject("test");
        
        if(spawnedObject == null)
        {
            return;
        }
        //(bool isCheck,int indexValue) = CheckRecipe(new BasicMaterialData());

        RecipeObject recipeObject = spawnedObject.GetComponent<RecipeObject>();

        recipeObject.InitRecipe(recipeId);

        reciptObjectLists.Add(recipeObject);
    }
    public (bool, int) CheckRecipe(BasicMaterialData reciptResult)
    {
        int index = 0;
        for(int i=0;i< reciptObjectLists.Count; i++)
        {

        }
        return ( false ,index) ;
    }
}
