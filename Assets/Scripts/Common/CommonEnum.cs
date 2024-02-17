#region ResourceType
public enum E_ANIMATION_EVENT
{
    CREATE_OBJECT,          //오브젝트 생성 이벤트
    CREATE_PARTICLE,        //파티클 생성 이벤트
    PLAY_SOUND,             //사운드 플레이 이벤트
    CAMERA_ACATION,         //카메라 이벤트

}
public enum E_SOUND_TYPE
{
    BGM_SOUND,
    VOICE_SOUND,
    FBX_SOUND,
}
#endregion ResourceType
#region EnviromentType
public enum E_APPLICATION_STATE
{
    APPLICATION_START,
    APPLICATION_UPDATE,
    REQUEST_PERMISSION,
    USER_LOGIN,
    BUNDLE_UPDATE,
    GAME_RESOURCE_LOAD,
    INAPP_UPDATE,
    TITLE,
}
public enum E_APPLICATION_PERMISSION_TYPE
{
    //퍼미션 타입
    STORAGE_PERMISSION,
    CAMERA_PERMISSION,
    END,
}
public enum E_DATABASE_TYPE
{
    USER_INFO,
    USER_STAGE,
    USER_ITEM,
    USER_OPTION,
    USER_LOBBYINFO,
}
#endregion EnviromentType
#region UserInfoType
public enum E_LOGIN_TYPE
{
    GUEST_LOGIN,
    GOOGLE_LOGIN,
    APPLE_LOGIN,
}
public enum E_LANGUAGE_TYPE
{
    //Default 는 한국어
    KOREAN,
    ENGLISH,
}
#endregion UserInfoType
#region UserDataType
public enum E_ITEM_TYPE
{
    GOLD,
    CRYSTALS,
    VALID_CRYSTALS,
}
#endregion UserDataType

public enum E_STOREITEM_TYPE
{
    CONSUME,
    NON_CONSUME,
    SUBSCRIPTION,
}
//서버에 보낼 행동 타입
public enum E_OBJECT_ACTION_TYPE
{
    ATTACK_OTHER,
    MOVE_SELF,
    MOVE_OTHER,
    HEAL_OTHER,
    DEFENCE_SELF,
}

#region IngameType
public enum E_INGAME_AI_TYPE
{
    NONE,
    UNIT_IDLE,
    UNIT_MOVE,
    UNIT_INTERACTION,
    UNIT_EVENT,

    END,
}
public enum E_STATVALUE_TYPE
{
    INTERACT_RANGE,
    UNIT_MOVE_SPEED,
    UNIT_ADVANTAGE_WORK_SPEED,
}
public enum E_STAT_DISPLAY_TYPE
{
    NONE,
    SKILL_DISPLAY,
    EVENT_DISPLAY,
}
public enum E_UNIT_AI_TYPE
{
    //Ingame AI Type
    MELEE_AI,
    RANGE_AI,

    //External Ai Type
    ANIMATION_AI = 100,//로비용 AI 로비외엔 생성 금지
}
#endregion IngameType