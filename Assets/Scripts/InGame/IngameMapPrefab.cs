using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMapPrefab : MonoBehaviour
{
    [SerializeField] private Transform[] team1Position;
    [SerializeField] private Transform[] team2Position;

    public Transform GetEnemyPos(int index)
    {
        return team2Position[index];
    }
    public Transform GetOurPos(int index)
    {
        return team1Position[index];
    }

    public int GetEnemyPosCount()
    {
        return team2Position.Length;
    }
    public int GetOurPosCount()
    {
        return team1Position.Length;
    }
}
