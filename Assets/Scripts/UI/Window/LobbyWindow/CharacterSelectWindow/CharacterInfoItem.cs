using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterInfoItem : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<CharacterInfoData>
{
    [SerializeField] CharacterSelectWindow characterSelectWindow; // 임시
    [SerializeField] Image chapterImage;
    [SerializeField] GameObject useStateObj;
    [SerializeField] GameObject lockStateObj;
    [SerializeField] GameObject selectedEffectObj;

    int currCharacterDataID = -1;

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
        currCharacterDataID = infos.data.ID;
        SetCharaterData();
    }

    public void SetCharaterData()
    {
        // 임시 값들
        isUseState = currCharacterDataID == characterSelectWindow.currUseCharacterID;
        isLockState = !(currCharacterDataID == 1 || currCharacterDataID == 2);

        useStateObj.SetActive(isUseState);
        lockStateObj.SetActive(isLockState);
        selectedEffectObj.SetActive(currCharacterDataID == characterSelectWindow.currSelectCharacterID);

        // 캐릭터 이미지 세팅해야 함

    }

    public void OnClickEvent()
    {
        if (isLockState)
            return;

        if (currCharacterDataID == -1)
        {
            Debug.LogError("currCharacterDataID가 초기화되지 않음");
            return;
        }

        characterSelectWindow.CharacterSelectEventCallBack(currCharacterDataID);
    }
}
