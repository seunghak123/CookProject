using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeData
{
    private List<int> recipeFoodList = new List<int>();
    public bool IsMakedRecipeFood(BasicMaterialData foodResult)
    {
        //이건 다른 곳에 필요
        List<int> resultFood = foodResult.GetFoodResult();

        resultFood.Sort();
        recipeFoodList.Sort();

        if (resultFood.Equals(recipeFoodList))
        {
            //같다면 //레시피 완성!
            return true;
        }
        return false;
    }
}
public class RecipeObject : MonoBehaviour
{
    [Header("Recipe UI")]
    [SerializeField] private Image recipeImage;
    [SerializeField] private Image recipeTimerProgressBar;

    private RecipeData recipe;

    private float currentTimer = 0.0f;
    public void InitRecipe(RecipeData targetRecipe)
    {
        recipe = targetRecipe;

        //레시피 Id에 따라서 Sprite 변경

        StartCoroutine(RecipeTimer());
    }
    private IEnumerator RecipeTimer()
    {
        while (true)
        {
            //5.0f의 경우는 레시피에 해당하는 타이머로 변경할 것
            if (currentTimer > 5.0f)
            {
                break;
            }
            currentTimer += Time.deltaTime;
            yield return WaitTimeManager.WaitForEndFrame();
        }
        yield break;
    }
    public bool CheckRecipe(BasicMaterialData foodResult)
    {
        if(recipe==null)
        {
            return false;
        }

        return recipe.IsMakedRecipeFood(foodResult);
    }
}
