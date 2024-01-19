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
        SetCharaterData(infos.data);
    }

    public void SetCharaterData(JCharacterData characterData)
    {
        currCharacterDataID = characterData.ID;

        // 임시로 갈겨놈
        isUseState = currCharacterDataID == 1;
        isLockState = !(currCharacterDataID == 1 || currCharacterDataID == 2);

        useStateObj.SetActive(isUseState);
        lockStateObj.SetActive(isLockState);

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

        characterSelectWindow.CharacterSelectEventCallBack(currCharacterDataID, this.transform);
    }
}
