using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeData
{
    private List<int> recipeFoodList = new List<int>();
    public void SetRecipeFoodResult(List<int> newRecipe)
    {
        recipeFoodList = newRecipe;
    }
    public bool IsMakedRecipeFood(BasicMaterialData foodResult)
    {
        //이건 다른 곳에 필요
        List<int> resultFood = foodResult.GetFoodResult();

        string resultCompareKey = IngameManager.currentManager.MakeRecipeStringKey(resultFood);

        string recipeCompareKey = IngameManager.currentManager.MakeRecipeStringKey(recipeFoodList);

        if (resultCompareKey.Equals(recipeCompareKey))
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
    private JRecipeData recipeData = null;
    private float currentTimer = 0.0f;
    public void InitRecipe(RecipeData targetRecipe,JRecipeData targetRecipeData)
    {
        recipe = targetRecipe;
        recipeData = targetRecipeData;

        int outputFood = recipeData.FoodOutput;
        JFoodObjectData foodData = JsonDataManager.Instance.GetFoodObject(outputFood);

        recipeImage.sprite = SpriteManager.Instance.LoadSprite(foodData.IconFile);

        StartCoroutine(RecipeTimer());
    }
    private IEnumerator RecipeTimer()
    {
        while (true)
        {
            if (currentTimer > recipeData.LimitTime)
            {
                //실패처리
                //IngameManager.
                break;
            }
            currentTimer += Time.deltaTime;
            yield return WaitTimeManager.WaitForEndFrame();
        }
        yield break;
    }
    public bool CheckRecipe(BasicMaterialData foodResult)
    {
        if(recipe==null || foodResult==null)
        {
            return false;
        }

        return recipe.IsMakedRecipeFood(foodResult);
    }
}
