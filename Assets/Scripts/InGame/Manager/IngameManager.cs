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
    public void CreateGame(int stageId)
    {

        //테스트 코드 작성
        JStageData stageData = JsonDataManager.Instance.GetStageData(0);

        Material targetMat = GameResourceManager.Instance.LoadObject(stageData.skyboxMat) as Material;
        GameObject spawnedMap = GameResourceManager.Instance.SpawnObject(stageData.mapPrefab);
        spawnedMap.transform.parent = mapSpawnPos;
        spawnedMap.transform.position = Vector3.zero;
        spawnedMap.transform.localPosition = Vector3.zero;

        mapPrefabs = spawnedMap.GetComponent<IngameMapPrefab>();

        if(mapPrefabs!=null)
        {
            //적 유닛 생성
            int[] unitdata = { 1, 3, 2 };
            List<JUnitData> enemyUnitData =new List<JUnitData>(JsonDataManager.Instance.GetUnitDatasArray(unitdata));

            for(int i=0;i< unitdata.Length; i++)
            {
                //차후 JUnitData가 아닌 JEnemyUnitData를 읽어오면, 다른 Structure을 생성가능
                JUnitData unitData = enemyUnitData.Find(index => index.index == unitdata[i]);
                GameObject spawnedEnemy = GameResourceManager.Instance.SpawnObject(unitData.unitPrefab);
                Transform spawnpos = mapPrefabs.GetEnemyPos(i);
                spawnedEnemy.transform.parent = spawnpos;
                spawnedEnemy.transform.localPosition = Vector3.zero;
                spawnedEnemy.transform.localRotation = Quaternion.Euler(Vector3.zero);
                UnitController enemyController = spawnedEnemy.GetComponent<UnitController>();

                enemyController.SetUnitInfo(unitData);
                enemyUnits.Add(enemyController);
            }
          

            //
        }

        //enemyDataJson 읽어서 적 데이터 가져올 것
        RenderSettings.skybox = targetMat;

        //ingameUI.InitGame(데이터)
        //스테이지 데이터를 받아서 게임 모듈 생성
    }
    public void CreateUnits(int deckId)
    {
        //유저 덱에 따라 유닛 생성
    }
}
public class IngameCreater
{
    public void CreateIngameObject(int objectId)
    {

    }
}