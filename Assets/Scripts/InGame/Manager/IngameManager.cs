using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Seunghak.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class RecipeResult
{
    public int toolNum;
    public int inputFoodNum;
}
public class IngameManager : MonoBehaviour
{
    public static IngameManager currentManager = null;
    public int currentScore = 0;
    public float currentTimer = 90.0f;
    public bool isStart = false;
    public bool isPause = false;
    public bool isGameEnd = false;
    [Header("SceneObject")]
    [SerializeField] private Transform mapSpawnPos = null;
    [SerializeField] private Transform uiSpawnPos = null;

    //인게임 오브젝트 
    private IngameMapPrefab mapPrefabs = null;
    private List<UnitController> enemyUnits = new List<UnitController>();
    private List<UnitController> teamUnits = new List<UnitController>();
    private BaseIngameUI ingameUI = null;
    private GameObject ingameMapObject = null;

    //인게임 관련 데이터
    private List<JRecipeData> recipeDataLists = new List<JRecipeData>();
    private Dictionary<int, RecipeResult> recipeToolOutputDic = new Dictionary<int, RecipeResult>();
    private Dictionary<string, int> recipeOutputDic = new Dictionary<string, int>();

    private List<JRecipeData> currentClearRecipe = new List<JRecipeData>();
    private JStageData currentStageData;
    private List<JRecipeData> currentCopyRecipeLists = new List<JRecipeData>();

    private List<GameObject> currentDroppedObjects = new List<GameObject>();
    private void Awake()
    {
        currentManager = this;
    }
    private void Update()
    {
        if(currentTimer<=0)
        {
            isGameEnd = true;
        }
    }
    public JStageData GetCurrentStageData()
    {
        return currentStageData;
    }
    private void GameClear()
    {
        ClearUI();
    }
    public bool IsPlaying()
    {
        return isStart && !isPause && !isGameEnd;
    }
    public void InitGame()
    {
        //필요한거 삭제 처리
    }
    private void ClearUI()
    {
        GameResourceManager.Instance.DestroyObject(ingameUI.gameObject);
        recipeDataLists.Clear();
        currentClearRecipe.Clear();
        currentCopyRecipeLists.Clear();
        currentStageData = null;
    }
    private void CreateRecipe(int recipeId)
    {
        JRecipeData newRecipe = JsonDataManager.Instance.GetRecipeData(recipeId);
        currentClearRecipe.Add(newRecipe);
        ingameUI.CreateRecipe(newRecipe);
    }
    private void CreateUserCharacter()
    {
        List<CharacterSpawnPosGimmick> spawnPos = new List<CharacterSpawnPosGimmick>
            (ingameMapObject.GetComponentsInChildren<CharacterSpawnPosGimmick>());


        for(int i=0;i<spawnPos.Count;i++)
        {
            
        }
    }
    private void CreatePreParticles()
    {
        //미리 파티클 모음 모아서 생성해 놓는다. 이건 수집형등에나 씀
    }
    public void CreateRandomRecipes()
    {
        UnityEngine.Random.InitState(((int)DateTime.Now.Ticks));
        for(int i=0;i< currentStageData.StartRecipeValue;i++)
        {
            int randomPos = UnityEngine.Random.Range(0, currentCopyRecipeLists.Count);

            CreateRecipe(currentCopyRecipeLists[randomPos].ID);
        }
    }
    public void CreateRandomRecipe()
    {
        int randomPos = UnityEngine.Random.Range(0, currentCopyRecipeLists.Count);

        CreateRecipe(currentCopyRecipeLists[randomPos].ID);
    }
    private BaseIngameUI CreateUI(int stageType)
    {
        BaseIngameUI baseUI = null;
        string createUIName = string.Empty;
        //LoadObject도 type에 따라 달리생성
        switch(stageType)
        {
            case 1:
                createUIName = "StoryUI";
                //stageType에따라서 ui명칭 따로 받아올것
                break;
            case 2:
                break;
        }

        GameObject createdUI = GameResourceManager.Instance.SpawnObject(createUIName);

        createdUI.transform.parent = uiSpawnPos;

        //UI 크기 확인
        RectTransform rectTransform = createdUI.GetComponent<RectTransform>();
        //Left, Bottom 변경
        rectTransform.offsetMin = new Vector2(0, 0);
        //Right, Top 변경 -> 원하는 크기 - 기준해상도값
        rectTransform.offsetMax = new Vector2(1, 1);

        recipeDataLists = JsonDataManager.Instance.GetRecipeLists();
        for(int i = 0; i < recipeDataLists.Count; i++)
        {
            string recipekey = string.Empty;
            if(recipeDataLists[i].Foodtype == 1)
            {
                List<int> recipeList = new List<int>();
                recipeList.Add(recipeDataLists[i].FoodName);
                recipekey = MakeRecipeStringKey(recipeList);
                recipeOutputDic[recipekey] = recipeDataLists[i].FoodOutput;

                RecipeResult result = new RecipeResult();
                result.inputFoodNum = recipeDataLists[i].FoodName;
                result.toolNum = recipeDataLists[i].UseObject;
                recipeToolOutputDic[recipeDataLists[i].FoodOutput] = result;
            }
            else if(recipeDataLists[i].Foodtype == 2)
            {
                List<int> recipeList = new List<int>(recipeDataLists[i].AddFood);
                recipekey = MakeRecipeStringKey(recipeList);
                recipeOutputDic[recipekey] = recipeDataLists[i].FoodOutput;
                if(recipeDataLists[i].ComplexFood!=null && recipeDataLists[i].ComplexFood.Length >= 1)
                {
                    List<int> recipeComplexList = new List<int>(recipeDataLists[i].ComplexFood);
                    string complexRecipekey = MakeRecipeStringKey(recipeComplexList);
                    recipeOutputDic[complexRecipekey] = recipeDataLists[i].FoodOutput;
                }
            }
        }

        //타입에 따라 불러오는걸 달리 해준다
        //차후 enum값으로 사용하게 변경
        //AddComponent해주고, -> Addcomponent 초기화-> 위치 세팅 ->
        switch (stageType)
        {
            case 1:
                StoryIngameUI storyIngameUI = createdUI?.GetComponent<StoryIngameUI>();
                StoryTimeInfo storyInfoData = new StoryTimeInfo();

                storyInfoData.timer = currentStageData.StageTimer;
                storyInfoData.storyString = currentStageData.Name;
                storyIngameUI.SetStoryTimeInfo(storyInfoData);
                baseUI = storyIngameUI;
                break;
            case 2:
                break;
        }

        return baseUI;
    }
    private void CreateSubUI(string uiName)
    {
        
    }
    private StringBuilder recipeBuilder = new StringBuilder();
    public string MakeRecipeStringKey(List<int> foodLists)
    {
        string makedLists = string.Empty;
        recipeBuilder.Clear();
        foodLists.Sort();
        for (int i=0;i<foodLists.Count;i++)
        {
            if (i != 0)
            {
                recipeBuilder.Append('_');
            }
            recipeBuilder.Append(foodLists[i]);
        }
        return recipeBuilder.ToString();
    }
    public RecipeResult GetOriginFoodData(int makedFoodId)
    {
        if(recipeToolOutputDic.ContainsKey(makedFoodId))
        {
            return recipeToolOutputDic[makedFoodId];
        }
        return null;
    }
    public void FailRecipe(int index)
    {
        int recipePos = index;

        if (currentClearRecipe.Count > recipePos)
        {
            currentClearRecipe.RemoveAt(recipePos);
            ingameUI.RemoveRecipe(false, recipePos);


            currentScore = currentScore - (int)(currentScore * 0.03f);
            //코인 감소
            CreateRandomRecipe();

            ingameUI.UpdateIngameData();
        }
    }
    public bool CheckRecipeComplete(BasicMaterialData completeFood)
    {
        if(ingameUI!=null)
        {
            (bool isResult, int recipePos) = ingameUI.CheckRecipe(completeFood);

            if(isResult)
            {

                currentClearRecipe.RemoveAt(recipePos);
                ingameUI.RemoveRecipe(true,recipePos);

                currentScore += recipeDataLists[recipePos].Score;
                int stageType = 1;
                switch (stageType)
                {
                    case 1:
                        StoryIngameUI storyIngameUI = ingameUI.GetComponent<StoryIngameUI>();
                        StoryScoreInfo storyInfoData = new StoryScoreInfo();

                        storyInfoData.ingameScore = currentScore;
                        storyIngameUI.SetStoryScoreInfo(storyInfoData);

                        CreateRandomRecipe();
                        break;
                    case 2:
                        break;
                }


                return true;
            }
            else
            {
                ingameUI.FailRecipe();

                return false;
            }
        }
        return false;
    }
    public void CreateCompleteCoin(int score)
    {
        int count = 1;
        int multiCount = 0;
        while(score>0)
        {
            score = score/10;
            multiCount++;
        }
        count = multiCount * 3 + count;

        //GameResourceManager.Instance.SpawnObject("")
    }
    public int GetRecipeFoodResult(List<int> foodResult)
    {
        string findKey = MakeRecipeStringKey(foodResult);
        if (recipeOutputDic.ContainsKey(findKey))
        {
            return recipeOutputDic[findKey];
        }
        else
        {
            if(foodResult.Count == 1)
            {
                return foodResult[0];
            }
            else
            {
                return 0;
            }
        }
    }
    public void SetDroppedObject(GameObject newObject)
    {
        if (currentDroppedObjects.Count>10)
        {
            GameResourceManager.Instance.DestroyObject(currentDroppedObjects[0]);
            currentDroppedObjects.RemoveAt(0);

            currentDroppedObjects.Add(newObject);
        }
    }
    public void RemoveDroppedObject(GameObject removeObject)
    {
        for(int i=0;i<currentDroppedObjects.Count;i++)
        {
            if(currentDroppedObjects[i] == removeObject)
            {
                GameResourceManager.Instance.DestroyObject(currentDroppedObjects[i]);
                currentDroppedObjects.RemoveAt(i);
                break;
            }
        }
    }
    public void CreateGame(int stageId)
    {
        //테스트 코드 작성
        currentStageData = JsonDataManager.Instance.GetStageData(stageId);
        currentCopyRecipeLists = JsonDataManager.Instance.GetRecipeGroupData(currentStageData.ProbabilityGroupID);
        currentTimer = currentStageData.StageTimer;
        ingameUI = CreateUI(1);
        CreateMapData(stageId);   
        //CreateUserCharacter();

        StartGame();
    }
    private async UniTask StartGame()
    {
        await UniTask.NextFrame();

        await ingameUI.StartDirection();

        //여기에 UI 킬거 킬 것,
        bool isRecipeEnd = false;
        StoryIngameRecipePopup ingameEndUI = GameResourceManager.Instance.SpawnObject("IngameRecipePopup").GetComponent<StoryIngameRecipePopup>();
        ingameEndUI.SetData(null, () => { isRecipeEnd = true; });


        while (!isRecipeEnd)
        {
            await UniTask.NextFrame();
        }
        //CinemachineTrack cinemachineTrack ;

        //if (cinemachineTrack != null)
        //{
        //    // 트랙의 길이를 가져와 대기
        //    float trackDuration = (float)cinemachineTrack.duration;
        //    await WaitTimeManager.WaitForTimeSeconds(trackDuration);

        //    // 시네머신 트랙 종료 후 실행할 코드 작성
        //    Debug.Log("Cinemachine Track Ended!");
        //}
        //여기서 특정 시네머신 대기

        //3~2~1 등

        isStart = true;
    }
    public void CreateMapData(int stageId)
    {
        if (mapSpawnPos.childCount != 0)
        {
            for (int i = mapSpawnPos.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(mapSpawnPos.GetChild(i).gameObject);
            }
        }
        if (ingameMapObject != null)
        {
            Destroy(ingameMapObject);
        }
        JStageData stageData = JsonDataManager.Instance.GetSingleData<JStageData>(stageId,E_JSON_TYPE.JStageData);
        UnityEngine.Object loadObject = GameResourceManager.Instance.LoadObject(stageData.StageFile);
        IngameMapObjectInfos loadData = JsonConvert.DeserializeObject<IngameMapObjectInfos>(loadObject.ToString());

        for(int i=0;i< loadData.objectLists.Count; i++)
        {
            IngameMapObject objectInfo = loadData.objectLists[i];
            if(objectInfo==null || objectInfo.objectPosition.Count!=3 || objectInfo.objectScale.Count !=3 || objectInfo.toolObjectId ==0)
            {
                continue;
            }
            Vector3 objectPos = new Vector3(objectInfo.objectPosition[0], objectInfo.objectPosition[1], objectInfo.objectPosition[2]);
            Vector3 objectScale = new Vector3(objectInfo.objectScale[0], objectInfo.objectScale[1], objectInfo.objectScale[2]);
            GameObject createdObject =  IngameCreater.CreateFoodObject(objectInfo.toolObjectId);
            if (createdObject != null)
            {
                createdObject.transform.parent = mapSpawnPos;
                createdObject.transform.position = objectPos;
                createdObject.transform.localScale = objectScale;
            }
        }
        GameObject stagePrefab = GameResourceManager.Instance.SpawnObject(stageData.StagePrefab);

        if(stagePrefab!=null)
        {
            stagePrefab.transform.parent = mapSpawnPos;
            stagePrefab.transform.position = Vector3.zero;
            stagePrefab.transform.localScale = Vector3.one;
        }

        ingameMapObject = stagePrefab;
    }
    public void ReplayStage()
    {
        GameClear();

        CreateGame(currentStageData.ID);
    }
}

public class IngameCreater
{
    public static GameObject CreateFoodObject(int foodObjectId)
    {
        // Json파일 읽기
        JToolObjectData foodObjectData = JsonDataManager.Instance.GetSingleData<JToolObjectData>(foodObjectId, E_JSON_TYPE.JToolObjectData);

        if(foodObjectData==null)
        {
            Debug.Log("food data null");
        }
        // SpawnObejct
        GameObject foodObject = GameResourceManager.Instance.SpawnObject($"{foodObjectData.ObjectFile}");

        // 타입에 따른 데이터 세팅
        SetFoodObject(foodObject, foodObjectData.Type);

        BaseToolObject foodObjectClass = foodObject.GetComponent<BaseToolObject>();

        if(foodObjectClass!=null)
        {
            foodObjectClass.OBJECT_ID = foodObjectId;
            foodObjectClass.SetToolData(foodObjectData);
        }

        //생성 되는거 확인
        return foodObject;
    }

    private static void SetFoodObject(GameObject foodObject, int type)
    {
        if(foodObject!=null)
        {
            foodObject.AddComponent(AddedComponentType(type)) ;
        }
    }
    private static System.Type AddedComponentType(int type)
    {
        switch (type)
        {
            case 1:
                return CommonUtil.GetTypeFromAssemblies("FoodStorageObject");
            case 2:
                return CommonUtil.GetTypeFromAssemblies("SingleToolObject");
            case 3: 
                return CommonUtil.GetTypeFromAssemblies("TableObject");
            case 4:
                return CommonUtil.GetTypeFromAssemblies("MultipleToolObject");
            case 5:
                return CommonUtil.GetTypeFromAssemblies("FoodDeliveryObject");
        }
        return null;
    }
}