using Newtonsoft.Json;
using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class IngameManager : MonoBehaviour
{
    public static IngameManager currentManager = null;
    public static int curentScore = 0;

    [Header("SceneObject")]
    [SerializeField] private Transform mapSpawnPos = null;
    [SerializeField] private Transform uiSpawnPos = null;
    [SerializeField] private Transform team1Parent = null;
    [SerializeField] private Transform team2Parent = null;

    private IngameMapPrefab mapPrefabs = null;
    private List<UnitController> enemyUnits = new List<UnitController>();
    private List<UnitController> teamUnits = new List<UnitController>();
    private BaseIngameUI ingameUI = null;

    private List<JRecipeData> recipeDataLists = new List<JRecipeData>();
    private Dictionary<string, int> recipeOutputDic = new Dictionary<string, int>();

    private List<JRecipeData> currentClearRecipe = new List<JRecipeData>();
    private void Awake()
    {
        currentManager = this;
    }
    public List<BaseAI> GetEnemyUnits()
    {
        List<BaseAI> enemyAIs = new List<BaseAI>();

        enemyAIs = enemyUnits.ConvertAll<BaseAI>(find => find.UNIT_AI);

        return enemyAIs;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CreateRecipe(1);
        }
    }
    private void CreateRecipe(int recipeId)
    {
        JRecipeData newRecipe = JsonDataManager.Instance.GetRecipeData(recipeId);
        currentClearRecipe.Add(newRecipe);
        ingameUI.CreateRecipe(newRecipe);
    }
    private BaseIngameUI CreateUI(int stageType)
    {
        BaseIngameUI baseUI = null;
        //LoadObject도 type에 따라 달리생성
        switch(stageType)
        {
            case 1:
                //stageType에따라서 ui명칭 따로 받아올것
                break;
            case 2:
                break;
        }

        GameObject createdUI = GameResourceManager.Instance.SpawnObject("StoryUI");

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
                baseUI = createdUI?.GetComponent<StoryIngameUI>();
                break;
            case 2:
                break;
        }

        return baseUI;
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
    public bool CheckRecipeComplete(BasicMaterialData completeFood)
    {
        if(ingameUI!=null)
        {
            (bool isResult, int recipePos) = ingameUI.CheckRecipe(completeFood);

            if(isResult)
            {

                currentClearRecipe.RemoveAt(recipePos);
                ingameUI.RemoveRecipe(recipePos);

                curentScore += recipeDataLists[recipePos].Score;

                return true;
            }
            else
            {
                //실패 했을 경우 벌칙 처리 필요
                return false;
            }
        }
        return false;
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
    public void CreateGame(int stageId)
    {
        //테스트 코드 작성
        JStageData stageData = JsonDataManager.Instance.GetStageData(1);

        ingameUI = CreateUI(1);
        CreateMapData(1);
        //enemyDataJson 읽어서 적 데이터 가져올 것
        //RenderSettings.skybox = targetMat;


        // 테스트 코드 (준혁)
        IngameCreater.CreateFoodObject(1001);
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
        JStageData stageData = JsonDataManager.Instance.GetSingleData<JStageData>(stageId,E_JSON_TYPE.JStageData);

        Object loadObject = GameResourceManager.Instance.LoadObject(stageData.StageFile);
        IngameMapObjectInfos loadData = JsonConvert.DeserializeObject<IngameMapObjectInfos>(loadObject.ToString());

        for(int i=0;i< loadData.objectLists.Count; i++)
        {
            IngameMapObject objectInfo = loadData.objectLists[i];
            if(objectInfo==null || objectInfo.objectPosition.Count!=3 || objectInfo.objectScale.Count !=3)
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

        BaseObject foodObjectClass = foodObject.GetComponent<BaseObject>();

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