using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CharacterInfoItem : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<CharacterInfoData>
{
    [SerializeField] CharacterSelectWindow characterSelectWindow; // 임시
    [SerializeField] Image chapterImage;
    [SerializeField] GameObject useStateObj;
    [SerializeField] GameObject lockStateObj;
    [SerializeField] GameObject selectedEffectObj;

    Action useCharacterIDCallBack = null;
    Action selectCharacterIDCallBack = null;

    int characterDataID = -1;

    bool isSelectState = false;
    bool isUseState = false;
    bool isLockState = true;

    public void OnPostSetupItems()
    {
        Button button = this.gameObject.GetOrAddComponent<Button>();
        button.transition = Selectable.Transition.None;
        button.onClick.AddListener(() => OnClickEvent());
    }

    public void OnUpdateItem(GameObject obj, CharacterInfoData infos)
    {
        characterDataID = infos.data.ID;
        SetCharaterData();
    }

    public void SetCallBackFunction(Action useCharacterIDCallBack, Action selectCharacterIDCallBack)
    {
        this.useCharacterIDCallBack = useCharacterIDCallBack;
        this.selectCharacterIDCallBack = selectCharacterIDCallBack;
    }

    public void SetCharaterData()
    {
        // 임시 값, 임시 처리
        isLockState = !(characterDataID == 1 || characterDataID == 2);
        isUseState = characterDataID == characterSelectWindow.CurrUseCharacterID;
        isSelectState = characterDataID == characterSelectWindow.CurrSelectCharacterID;

        lockStateObj.SetActive(isLockState);
        useStateObj.SetActive(isUseState);
        selectedEffectObj.SetActive(isSelectState);

        // 캐릭터 이미지 세팅해야 함

    }

    public void OnClickEvent()
    {
        if (isLockState || isSelectState)
            return;

        if (characterDataID == -1)
        {
            Debug.LogError("currCharacterDataID가 초기화되지 않음");
            return;
        }

        characterSelectWindow.CharacterSelectEventCallBack(characterDataID);
    }
}
