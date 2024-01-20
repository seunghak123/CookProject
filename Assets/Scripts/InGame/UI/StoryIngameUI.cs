using Cysharp.Threading.Tasks;
using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class StoryTimeInfo
{
    public float timer = 0.0f;
    public string storyString;
}
public class StoryScoreInfo
{
    public int ingameScore = 0;
}
[System.Serializable]
public class PlayingTimeInfo
{
    public float playingTime = 0.0f;
    public Animation playingAnim;
}
public class StoryIngameUI : BaseIngameUI
{
    protected List<RecipeObject> reciptObjectLists = new List<RecipeObject>();
    [SerializeField] protected TextMeshProUGUI ingameTimer;
    [SerializeField] protected TextMeshProUGUI ingameScore;
    [SerializeField] private List<PlayingTimeInfo> playingAnim = new List<PlayingTimeInfo>();
    [SerializeField] private Transform startUITransform;

    private float currentTimer = 0.0f;
    private int currentScore = 0;
    public void StartStoryRefresh()
    {
        StartCoroutine(StoryInfoRefresh());
    }
    public void SetStoryTimeInfo(StoryTimeInfo storyInfo)
    {
        currentTimer = storyInfo.timer;
        RefreshIngameUI();
    }
    public void SetStoryScoreInfo(StoryScoreInfo storyInfo)
    {
        currentScore = storyInfo.ingameScore;
        RefreshIngameUI();
    }
    private IEnumerator StoryInfoRefresh()
    {
        while(true)
        {
            if (IngameManager.currentManager.IsPlaying())
            {
                currentTimer -= Time.fixedDeltaTime;
                IngameManager.currentManager.currentTimer = currentTimer;
                RefreshIngameUI();
            }

            if(IngameManager.currentManager.isGameEnd)
            {
                break;
            }
            yield return WaitTimeManager.WaitForRealTimeSeconds(Time.fixedDeltaTime);
        }
        yield break;
    }
    public override async UniTask StartDirection()
    {
        startUITransform.gameObject.SetActive(true);
        await UniTask.NextFrame();
        float directTimer = 0.0f;
        bool isEnd = false;
        while (!isEnd)
        {
            bool doEnd = true;
            for (int i=0;i< playingAnim.Count;i++)
            {
                if (playingAnim[i].playingAnim.clip.length > directTimer && playingAnim[i].playingTime < directTimer)
                {
                    if (playingAnim[i].playingAnim.isPlaying)
                    {
                        playingAnim[i].playingAnim.Play();
                    }
                }

                if(playingAnim[i].playingAnim.clip.length + playingAnim[i].playingTime > directTimer)
                {
                    doEnd = doEnd & false;
                    continue;
                }
            }
            if (doEnd)
            {
                isEnd = true;
            }
            directTimer += Time.fixedDeltaTime;
            await WaitTimeManager.WaitForRealTimeSeconds(Time.fixedDeltaTime);
        }


        startUITransform.gameObject.SetActive(false);
        StartStoryRefresh();
    }
    public override void UpdateIngameData()
    {

    }
    public override void RefreshIngameUI()
    {
        ingameTimer.text = CommonUtil.GetTimerString(currentTimer);
        ingameScore.text = $"{currentScore}";
    }
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
        spawnedObject.transform.parent = reciptParent;

        RecipeObject recipeObject = spawnedObject.GetComponent<RecipeObject>();

        RecipeData createdRecipe = new RecipeData();
        List<int> recipeLists = new List<int>(recipeData.AddFood);

        createdRecipe.SetRecipeFoodResult(recipeLists);
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
        return ( false ,index) ;
    }
    public int CompleteFoodResult(int index)
    {
        RecipeObject removeRecipe = reciptObjectLists[index];

        reciptObjectLists.RemoveAt(index);

        int score = 10;

        GameResourceManager.Instance.DestroyObject(removeRecipe.gameObject);

        return score;
    }
}
