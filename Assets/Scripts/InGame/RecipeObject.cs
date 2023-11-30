using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeObject : MonoBehaviour
{
    private int recipeId = 0;
    private List<int> recipeFoodList = new List<int>();
    public void InitRecipe(int targetRecipeId)
    {
        recipeId = targetRecipeId;
    }

    public bool IsMakedRecipeFood(BasicMaterialData foodResult)
    {
        List<int> resultFood = foodResult.GetFoodResult();

        return false;
    }
}
