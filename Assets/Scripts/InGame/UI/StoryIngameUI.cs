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
        //recipeId�� ���ؼ� recipeData ����

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
        //�� �������� ��쿣 ��� ó���ϴ°�?
        return ( false ,index) ;
    }

    public int CompleteFoodResult(int index)
    {
        RecipeObject removeRecipe = reciptObjectLists[index];

        reciptObjectLists.RemoveAt(index);

        int score = 10;
        //�������� ī�����ؼ� 
        GameResourceManager.Instance.DestroyObject(removeRecipe.gameObject);

        return score;
    }
}
