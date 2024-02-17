using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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
[Serializable]
public class RecipeInfos
{
    [SerializeField] public Image foodImages;
    [SerializeField] public Image toolImages;
}
public class RecipeObject : MonoBehaviour
{
    [Header("Recipe UI")]
    [SerializeField] private RectTransform totalTransform;
    [SerializeField] private Image recipeImage;
    [SerializeField] private Image recipeTimerProgressBar;
    [SerializeField] private List<RecipeInfos> recipeImages;

    [Header("Recipe Animation Properties")]
    [SerializeField] private float createTimer = 0.4f;
    [SerializeField] private float moveTimer = 0.4f;
    private Vector2 moveVector = Vector2.zero;
    private RecipeData recipe;
    private JRecipeData recipeData = null;
    private float currentTimer = 0.0f;
    private int currentIndex = 0;
    public void OnDisable()
    {
        StopAllCoroutines();
    }
    public void AddCurrentTimer(float addTime)
    {
        //반짝임 애님 실행
        currentTimer += addTime;
    }
    public void SetIndex(int index)
    {
        currentIndex = index;
    }
    public void InitRecipe(RecipeData targetRecipe,JRecipeData targetRecipeData)
    {
        moveVector = new Vector2(0, this.GetComponent<RectTransform>().sizeDelta.y);
        recipe = targetRecipe;
        recipeData = targetRecipeData;

        int outputFood = recipeData.FoodOutput;
        JFoodObjectData foodData = JsonDataManager.Instance.GetFoodObject(outputFood);

        recipeImage.sprite = SpriteManager.Instance.LoadSprite(foodData.IconFile);
        SetRecipeLists(targetRecipeData);
        StartCoroutine(RecipeTimer());
    }
    private void SetRecipeLists(JRecipeData targetRecipeData)
    {
        //grid를 위해서 전부 active 꺼주기
        for (int i = 0; i < 4; i++)
        {
            recipeImages[i].foodImages.gameObject.SetActive(false);
            recipeImages[i].toolImages.gameObject.SetActive(false);
        }

        for(int i=0;i< targetRecipeData.AddFood.Length; i++)
        {
            RecipeResult toolComplex = IngameManager.currentManager.GetOriginFoodData(targetRecipeData.AddFood[i]);

            if(toolComplex!=null)
            {
                recipeImages[i].foodImages.gameObject.SetActive(true);
                recipeImages[i].toolImages.gameObject.SetActive(true);


                string foodImage = JsonDataManager.Instance.GetFoodObject(toolComplex.inputFoodNum).IconFile;
                string toolImage = JsonDataManager.Instance.GetToolId(toolComplex.toolNum).IconFile;


                recipeImages[i].foodImages.sprite = SpriteManager.Instance.LoadSprite(foodImage);
                recipeImages[i].toolImages.sprite = SpriteManager.Instance.LoadSprite(toolImage);
            }
            else
            {
                recipeImages[i].foodImages.gameObject.SetActive(true);
                recipeImages[i].toolImages.gameObject.SetActive(false);

                string foodImage = JsonDataManager.Instance.GetFoodObject(targetRecipeData.AddFood[i]).IconFile;
                recipeImages[i].foodImages.sprite = SpriteManager.Instance.LoadSprite(foodImage);
            }
        }
    }
    public void Reposition(int index)
    {
        moveVector = new Vector2((index - currentIndex) * this.GetComponent<RectTransform>().sizeDelta.x , 0);
        StartCoroutine(RecipeMove());
        currentIndex = index;
    }
    private IEnumerator RecipeMove()
    {
        totalTransform.transform.localPosition = moveVector;
        float recentTime = moveTimer;
        while (recentTime > 0)
        {
            recentTime -= Time.deltaTime;

            totalTransform.transform.localPosition = moveVector * (recentTime/ moveTimer);

            yield return WaitTimeManager.WaitForEndFrame();
        }
        totalTransform.transform.position = Vector2.zero;
    }
    private IEnumerator RecipeTimer()
    {
        totalTransform.transform.localPosition = moveVector;
        float recentTime = createTimer;
        while (recentTime > 0)
        {
            recentTime -= Time.deltaTime;

            totalTransform.transform.position =  moveVector * (recentTime / createTimer);

            yield return WaitTimeManager.WaitForEndFrame();
        }
        totalTransform.transform.localPosition = Vector2.zero;
        while (true)
        {
            if (currentTimer > recipeData.LimitTime)
            {
                IngameManager.currentManager.FailRecipe(currentIndex);
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
