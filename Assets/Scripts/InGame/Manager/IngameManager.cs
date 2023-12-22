using Newtonsoft.Json;
using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IngameManager : MonoBehaviour
{
    public static IngameManager currentManager = null;
    public static int curentScore = 0;

    [Header("SceneObject")]
    [SerializeField] private Transform mapSpawnPos = null;
    [SerializeField] private Transform team1Parent = null;
    [SerializeField] private Transform team2Parent = null;

    private IngameMapPrefab mapPrefabs = null;
    private List<UnitController> enemyUnits = new List<UnitController>();
    private List<UnitController> teamUnits = new List<UnitController>();
    private BaseIngameUI ingameUI = null;

    private List<JRecipeData> recipeDataLists = new List<JRecipeData>();
    private Dictionary<List<int>, int> recipeOutputDic = new Dictionary<List<int>, int>();
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

        GameObject createdUI = GameResourceManager.Instance.LoadObject("StoryUI") as GameObject;



        recipeDataLists = JsonDataManager.Instance.GetRecipeLists();
        for(int i = 0; i < recipeDataLists.Count; i++)
        {
            if(recipeDataLists[i].Foodtype == 1)
            {
                List<int> recipeList = new List<int>(recipeDataLists[i].FoodName);
                recipeOutputDic[recipeList] = recipeDataLists[i].UseObjectOutput;
            }
            else if(recipeDataLists[i].Foodtype == 2)
            {
                List<int> recipeList = new List<int>(recipeDataLists[i].AddFood);
                recipeOutputDic[recipeList] = recipeDataLists[i].AddOuput;
                if(recipeDataLists[i].ComplexFood!=null && recipeDataLists[i].ComplexFood.Length >= 1)
                {
                    List<int> recipeComplexList = new List<int>(recipeDataLists[i].ComplexFood);
                    recipeOutputDic[recipeComplexList] = recipeDataLists[i].AddOuput;
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
    public bool CheckRecipeComplete(BasicMaterialData completeFood)
    {
        if(ingameUI!=null)
        {
            (bool isResult, int recipePos) = ingameUI.CheckRecipe(completeFood);

            if(isResult)
            {
                //레시피 체크가 성공했을때
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
        if(recipeOutputDic.ContainsKey(foodResult))
        {
            return recipeOutputDic[foodResult];
        }
        else
        {
            return 0;
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
    }
}

public class IngameCreater
{
    public static GameObject CreateFoodObject(int foodObjectId)
    {
        // Json파일 읽기
        JToolObjectData foodObjectData = JsonDataManager.Instance.GetSingleData<JToolObjectData>(foodObjectId, E_JSON_TYPE.JToolObjectData);

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