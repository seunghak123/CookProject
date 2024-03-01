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
    [SerializeField] Image unlockImage;
    [SerializeField] TextMeshProUGUI stageNameText;

    Action<int> OnCilckStageElementCallBack = null;
    JStageData stageData;

    public void Init(int stageDataID, bool isLockState, Action<int> OnCilckStageElementCallBack)
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

        lockImage.gameObject.SetActive(isLockState);
        unlockImage.gameObject.SetActive(!isLockState);
    }

    public void OnClickEvnet()
    {
        OnCilckStageElementCallBack?.Invoke(stageData.ID);
    }
}
