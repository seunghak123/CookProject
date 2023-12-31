using Seunghak.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class StageElement : MonoBehaviour
    {
        [SerializeField] Image stageLockStateImage;
        [SerializeField] TextMeshProUGUI stageNameText;

        public bool IsLock { get; private set; } = false; // 세팅 필요

        Action<int> OnStageClickEvent;
        int stageID = -1;

        public void SetStageInfo(Action<int> OnStageClickEvent, int stageID)
        {
            this.OnStageClickEvent = OnStageClickEvent;
            this.stageID = stageID;

            string stageName = JsonDataManager.LoadJsonDatas<JStageData>(E_JSON_TYPE.JStageData)[stageID.ToString()].Name;

            if (stageName == null)
            {
                Debug.Log($"{stageID}번 스테이지 데이터가 비어있음");
                return;
            }

            stageNameText.text = stageName;

            // 이전 스테이지가 존재할 경우, 이전 스테이지를 확인해 잠금여부 이미지 세팅
        }

        public void OnClickEvent()
        {
            if(OnStageClickEvent == null || stageID == -1)
            {
                Debug.LogError("콜백함수가 null이거나 스테이지아이디가 세팅되지 않음");
            }

            // if (isLock) return;

            OnStageClickEvent.Invoke(stageID);
        }
    }
}
