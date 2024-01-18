using Seunghak.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioStageElement : MonoBehaviour
{
    [SerializeField] Image lockImage;
    [SerializeField] TextMeshProUGUI stageNameText;

    Action<int> OnCilckStageElementCallBack = null;
    JStageData stageData;

    public void Init(int stageDataID, Action<int> OnCilckStageElementCallBack)
    {
        JStageData stageData = JsonDataManager.Instance.GetStageData(stageDataID);

        if(stageData == null)
        {
            Debug.LogError("StageData is Null!!");
            return;
        }

        this.stageData = stageData;
        this.OnCilckStageElementCallBack = OnCilckStageElementCallBack;

        stageNameText.text = stageData.Name;

        // 잠금상태 확인해서 적용하고, 잠금여부를 체크해야 함

    }

    public void OnClickEvnet()
    {
        if (stageData != null)
            OnCilckStageElementCallBack?.Invoke(stageData.ID);
    }
}
