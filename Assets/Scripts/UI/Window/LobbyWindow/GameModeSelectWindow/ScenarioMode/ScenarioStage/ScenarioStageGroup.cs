using Seunghak.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ScenarioStageGroup : MonoBehaviour
    {
        [SerializeField] ScenarioStageElement[] scenarioStageElements = new ScenarioStageElement[5];

        public void Init(int chapterDataID, Action<int> OnCilckStageElementCallBack)
        {
            // 첫 번째 스테이지 데이터 수식(?)
            int firstStageDataID = (chapterDataID - 1) * 5 + 1;

            for (int i = 0; i < scenarioStageElements.Length; i++)
            {
                bool isLockState = !UserDataManager.Instance.IsScenarioStageResult(firstStageDataID + i - 1);
                scenarioStageElements[i].Init(firstStageDataID + i, isLockState, OnCilckStageElementCallBack);
            }
        }
    }
}
