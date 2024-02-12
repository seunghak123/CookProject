using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnPosGimmick : BaseGimmick
{
    [SerializeField] private int spawnPos = 0;

    public int GetSpawnPos()
    {
        return spawnPos;
    }
}
