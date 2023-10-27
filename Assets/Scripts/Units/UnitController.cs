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
        JUnitData unitData = JsonDataManager.Instance.GetUnitData(unitId);

        UnitStructure newUnitStructure = new UnitStructure(unitData);

        //유닛데이터 지정하고

        AttachAIComponent((E_UNIT_AI_TYPE)unitData.aiType);
        //AI 
        return true;
    }
    public bool SetUnitInfo(JUnitData getunitData)
    {
        JUnitData unitData = getunitData;

        UnitStructure newUnitStructure = new UnitStructure(unitData);

        //유닛데이터 지정하고

        AttachAIComponent((E_UNIT_AI_TYPE)unitData.aiType);
        //AI 
        return true;
    }
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
    public void SetTeamInfo(E_INGAME_TEAM_TYPE teamType)
    {
        if (unitAI != null)
        {
            unitAI.SetTeamType(teamType);
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
    public Dictionary<E_STATVALUE_TYPE, EquipReasonInfo> equipsUnitInfo = new Dictionary<E_STATVALUE_TYPE, EquipReasonInfo>();
    //특정한 이벤트에 의해서 추가된 값
    public List<AddedReasonInfo> addedUnitInfo = new List<AddedReasonInfo>();
    public float GetStatValue(E_STATVALUE_TYPE getUnitInfoType)
    {
        CalcStatValue();
        if (totalUnitInfo.ContainsKey(getUnitInfoType))
        {
            return totalUnitInfo[getUnitInfoType];
        }

        return 0.0f;
    }
    public void CalcStatValue()
    {
        //계산 공식은 어찌할 것인가? 모든 addPercent합으로 할 것인가, 아니면 
        //percent는 따로따로 해서 할것인가
        foreach (var infovalue in baseUnitInfo)
        {
            switch (infovalue.Key)
            {
                //추후 Percent로 올리는 부분이 있으면 올려준다
                case E_STATVALUE_TYPE.HP_VALUE:

                    totalUnitInfo[infovalue.Key] = infovalue.Value;
                    totalUnitInfo[E_STATVALUE_TYPE.MAX_HP_VALUE] = infovalue.Value;
                    break;
                default:
                    totalUnitInfo[infovalue.Key] = infovalue.Value;
                    break;
            }
        }
        foreach (var addedValue in equipsUnitInfo)
        {
            switch (addedValue.Key)
            {
                //추후 Percent로 올리는 부분이 있으면 올려준다
                //case E_STATVALUE_TYPE.ATTACK_VALUE
                default:
                    if (totalUnitInfo.ContainsKey(addedValue.Key))
                    {
                        totalUnitInfo[addedValue.Key] += addedValue.Value.statValue;
                    }
                    else
                    {
                        //TotalUnitInfo에는 최종적으로 계산된 값만 넣을것
                        totalUnitInfo[addedValue.Key] = addedValue.Value.statValue;
                    }
                    break;
            }
        }
    }

    //기본 데이터 받고 차후 추가 데이터 지정
    public UnitStructure(JUnitData unitData)
    {
        baseUnitInfo[E_STATVALUE_TYPE.ATTACK_VALUE] = unitData.unitAttack;
        baseUnitInfo[E_STATVALUE_TYPE.ATTACK_RANGE] = unitData.unitAttackRange;
        baseUnitInfo[E_STATVALUE_TYPE.HP_VALUE] = unitData.unitHp;
        baseUnitInfo[E_STATVALUE_TYPE.MAX_HP_VALUE] = unitData.unitHp;
        baseUnitInfo[E_STATVALUE_TYPE.DEFENCE_VALUE] = unitData.unitDefence;
        baseUnitInfo[E_STATVALUE_TYPE.CRITICAL_PERCENT_VALUE] = unitData.unitCriticalPercent;

        //레벨에 따라 유닛 데이터 세팅... 레벨 몇이면 hp 몇 이렇게
        //

        //장비 데이터 추가
        //밑의 AddedReasonInfo는 장비로 증가한것들
        EquipReasonInfo addedReason = new EquipReasonInfo();
        addedReason.statValue = 30.0f;
        addedReason.statNameString = "기본 스탯 퍼센트";

        //동시에 2개의 스탯을 올릴떄가 대비가 X - 방안 찾아볼것
        EquipReasonInfo copiedReason = addedReason;
        copiedReason.statValue = 50.0f;

        //
    }

    //컴퓨터 AI의 경우는 기존 캐릭터와 다른 데이터 구조가 필요하기 떄문에 따로 엑셀파일
    //분리하여 사용
    //public UnitStructure(JEnemyUnitData unitData)
    //{

    //}
}
//시간제의 경우 IngameTimeManager에서 제거 해준다
public struct EquipReasonInfo
{
    public string statNameString;
    public string statValueString;
    public float statValue;
}
public class AddedReasonInfo
{
    public E_STAT_DISPLAY_TYPE displayType = E_STAT_DISPLAY_TYPE.NONE;
    public Texture addedReasonTexture;
    public string addedReasonNameString;
    public string addedReasonValueString;
    public Dictionary<E_STATVALUE_TYPE, float> addedValueInfo = new Dictionary<E_STATVALUE_TYPE, float>();
}
#endregion