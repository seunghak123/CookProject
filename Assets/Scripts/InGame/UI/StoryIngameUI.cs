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
    protected List<RecipeObject> recipeObjectLists = new List<RecipeObject>();
    [SerializeField] protected TextMeshProUGUI ingameTimer;
    [SerializeField] protected TextMeshProUGUI ingameScore;
    [SerializeField] private List<PlayingTimeInfo> playingAnim = new List<PlayingTimeInfo>();
    [SerializeField] private Transform startUITransform;

    [Header("recipe properties")]
    [SerializeField] private float recipeDistance = 120f;
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
                StoryIngameEndUI ingameEndUI = GameResourceManager.Instance.SpawnObject("IngameEnd").GetComponent<StoryIngameEndUI>();
                ingameEndUI.gameObject.transform.parent = this.transform;
                ingameEndUI.gameObject.transform.localPosition = Vector3.zero;
                ingameEndUI.gameObject.transform.localScale = Vector3.one;

                StoryEndData storyData = new StoryEndData(IngameManager.currentManager.GetCurrentStageData(), IngameManager.currentManager.currentScore);
                ingameEndUI.SetEndInfo(storyData);
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

        IngameManager.currentManager.CreateRandomRecipes();

        await WaitTimeManager.WaitForRealTimeSeconds(0.5f);
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
    public override void RepositionRecipe()
    {
        //레시피 위치로 이동
    }
    public override void RemoveRecipe(bool isSuccess = false,int index = 0)
    {
        //우선 해당 오브젝트 뭐시기 하고
        if(isSuccess)
        {
            //여기에 성공 연출 대기
        }
        else
        {
            //여기에 실패 연출 대기
        }
        //사라지게 하기 및에는 코루틴이나 task로 넘길것
        GameResourceManager.Instance.DestroyObject(recipeObjectLists[index].gameObject);
        recipeObjectLists.RemoveAt(index);
        for(int i=0;i<recipeObjectLists.Count;i++)
        {
            recipeObjectLists[i].Reposition(i);
        }
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
        recipeObject.SetIndex(recipeObjectLists.Count);        
        recipeObject.InitRecipe(createdRecipe,recipeData);
        recipeObjectLists.Add(recipeObject);
    }
    public override (bool, int) CheckRecipe(BasicMaterialData reciptResult)
    {
        int index = 0;
        for(int i=0;i< recipeObjectLists.Count; i++)
        {
            if(recipeObjectLists[i].CheckRecipe(reciptResult))
            {
                index = i;
                return (true, index);
            }
        }
        return ( false ,index) ;
    }
    public int CompleteFoodResult(int index)
    {
        RecipeObject removeRecipe = recipeObjectLists[index];

        recipeObjectLists.RemoveAt(index);

        int score = 10;

        GameResourceManager.Instance.DestroyObject(removeRecipe.gameObject);

        return score;
    }
    public override void FailRecipe()
    {
        int randomRecipe = UnityEngine.Random.Range(0, recipeObjectLists.Count);
        RecipeObject removeRecipe = recipeObjectLists[randomRecipe];

        removeRecipe.AddCurrentTimer(5.0f);
    }
}
