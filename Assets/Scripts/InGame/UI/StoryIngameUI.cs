using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIngameUI : BaseIngameUI
{
    protected List<RecipeObject> reciptObjectLists = new List<RecipeObject>();
    public override void CreateRecipe(int recipeId)
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
        //recipeId�� ���ؼ� recipeData ����

        recipeObject.InitRecipe(createdRecipe);

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
