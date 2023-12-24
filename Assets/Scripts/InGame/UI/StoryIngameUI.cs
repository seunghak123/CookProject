using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIngameUI : BaseIngameUI
{
    protected List<RecipeObject> reciptObjectLists = new List<RecipeObject>();
    public override void RemoveRecipe(int index = 0)
    {
        GameResourceManager.Instance.DestroyObject(reciptObjectLists[index].gameObject);
        reciptObjectLists.RemoveAt(index);
    }
    public override void CreateRecipe(JRecipeData recipeData)
    {
        GameObject spawnedObject = GameResourceManager.Instance.SpawnObject("StoryRecipe");
        
        if(spawnedObject == null)
        {
            return;
        }
        //(bool isCheck,int indexValue) = CheckRecipe(new BasicMaterialData());
        
        spawnedObject.transform.parent = reciptParent;

        RecipeObject recipeObject = spawnedObject.GetComponent<RecipeObject>();

        RecipeData createdRecipe = new RecipeData();
        List<int> recipeLists = new List<int>(recipeData.AddFood);

        createdRecipe.SetRecipeFoodResult(recipeLists);
        //recipeId를 통해서 recipeData 추출

        recipeObject.InitRecipe(createdRecipe,recipeData);

        reciptObjectLists.Add(recipeObject);
    }
    public override (bool, int) CheckRecipe(BasicMaterialData reciptResult)
    {
        int index = 0;
        for(int i=0;i< reciptObjectLists.Count; i++)
        {
            if(reciptObjectLists[i].CheckRecipe(reciptResult))
            {
                index = i;
                return (true, index);
            }
        }
        //다 실패했을 경우엔 어떻게 처리하는가?
        return ( false ,index) ;
    }

    public int CompleteFoodResult(int index)
    {
        RecipeObject removeRecipe = reciptObjectLists[index];

        reciptObjectLists.RemoveAt(index);

        int score = 10;
        //점수등을 카운팅해서 
        GameResourceManager.Instance.DestroyObject(removeRecipe.gameObject);

        return score;
    }
}
