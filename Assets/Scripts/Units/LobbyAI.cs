﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyAI : BaseAI
{
    private List<E_INGAME_AI_TYPE> playAnimAiLists = new List<E_INGAME_AI_TYPE>()
    {
        E_INGAME_AI_TYPE.UNIT_EVENT,
        E_INGAME_AI_TYPE.UNIT_IDLE,
    };
    public void PlayAnim()
    {
        int random = Random.Range(0, playAnimAiLists.Count);
        unitAIType = E_INGAME_AI_TYPE.NONE;
        ChangeAI(playAnimAiLists[random]);
    }
    public override void FixedUpdate()
    {
        //Update문을 실행하지 않기 위한 override
    }
}
