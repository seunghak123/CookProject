using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IngameManager : MonoBehaviour
{
    public static IngameManager currentManager = null;
    [Header("SceneObject")]
    [SerializeField] private Transform mapSpawnPos = null;
    [SerializeField] private Transform team1Parent = null;
    [SerializeField] private Transform team2Parent = null;

    private IngameMapPrefab mapPrefabs = null;
    private List<UnitController> enemyUnits = new List<UnitController>();
    private List<UnitController> teamUnits = new List<UnitController>();
    private BaseIngameUI ingameUI = null;
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
    private void CheckRecipeComplete(BasicMaterialData completeFood)
    {
        if(ingameUI!=null)
        {
            (bool isResult, int recipePos) = ingameUI.CheckRecipe(completeFood);

        }
    }
    public void CreateGame(int stageId)
    {
        //테스트 코드 작성
        JStageData stageData = JsonDataManager.Instance.GetStageData(0);

        ingameUI = CreateUI(1);


        //enemyDataJson 읽어서 적 데이터 가져올 것
        //RenderSettings.skybox = targetMat;

    }
    public void CreateUnits(int deckId)
    {
        //유저 덱에 따라 유닛 생성
    }
}
public class IngameCreater
{
    public static void CreateIngameObject(int objectId)
    {
        //여기에 Json파일 읽고 , SpawnObject-> AddComponent, -> Component에 따라 세팅하고,
        //생성 되는거 확인
    }
}