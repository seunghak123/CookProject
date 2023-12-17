using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private BaseAI unitAI;
    public BaseAI UNIT_AI
    {
        get { return unitAI; }
    }
    //
    //[SerializeField] private BaseUnitUI unitUI;
    //생성시 유닛 고유 식별 ID
    private long unitIntanceId;
    public void CreateLobbyAI()
    {
        AttachAIComponent(E_UNIT_AI_TYPE.ANIMATION_AI);
    }
    //AI Type받고 해당 AI 생성
    public bool SetUnitInfo(int unitId)
    {
        //JUnitData unitData = JsonDataManager.Instance.GetUnitData(unitId);


        //유닛데이터 지정하고

        //AttachAIComponent((E_UNIT_AI_TYPE)unitData.aiType);
        //AI 
        return true;
    }
    //public bool SetUnitInfo(JUnitData getunitData)
    //{
    //    JUnitData unitData = getunitData;


    //    //유닛데이터 지정하고

    //    AttachAIComponent((E_UNIT_AI_TYPE)unitData.aiType);
    //    //AI 
    //    return true;
    //}
    private void AttachAIComponent(E_UNIT_AI_TYPE aiType)
    {
        BaseAI addedAI = null;
        switch (aiType)
        {
            case E_UNIT_AI_TYPE.MELEE_AI:
                addedAI = this.gameObject.AddComponent<BaseAI>();
                break;
            case E_UNIT_AI_TYPE.RANGE_AI:
                addedAI = this.gameObject.AddComponent<RangeAI>();
                break;
            case E_UNIT_AI_TYPE.ANIMATION_AI:
                addedAI = this.gameObject.AddComponent<LobbyAI>();
                break;
        }
        if (addedAI != null)
        {
            unitAI = addedAI;
        }
    }
}
#region UnitStruct
public class UnitStructure
{
    //기본 유닛 정보 값
    public Dictionary<E_STATVALUE_TYPE, float> baseUnitInfo = new Dictionary<E_STATVALUE_TYPE, float>();

    //계산된 유닛 정보 값(버프등)
    public Dictionary<E_STATVALUE_TYPE, float> totalUnitInfo = new Dictionary<E_STATVALUE_TYPE, float>();

    //추가된 유닛 정보 값
    public Dictionary<E_STATVALUE_TYPE, AddedReasonInfo> equipsUnitInfo = new Dictionary<E_STATVALUE_TYPE, AddedReasonInfo>();
    public float GetStatValue(E_STATVALUE_TYPE getUnitInfoType)
    {
        CalcStatValue();

        if (totalUnitInfo.ContainsKey(getUnitInfoType))
        {
            return totalUnitInfo[getUnitInfoType];
        }

        return 0.0f;
    }
    private void CalcStatValue()
    {

    }
    //컴퓨터 AI의 경우는 기존 캐릭터와 다른 데이터 구조가 필요하기 떄문에 따로 엑셀파일
    //분리하여 사용
    //public UnitStructure(JEnemyUnitData unitData)
    //{

    //}
}
public struct AddedReasonInfo
{
    public string statNameString;
    public string statValueString;
    public float statValue;
}
#endregion