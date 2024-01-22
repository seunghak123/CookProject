using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Seunghak.Common;
using System;

namespace Seunghak.UIManager
{
    public class CharacterInfoMenu : MonoBehaviour
    {
        [SerializeField] CharacterSelectWindow characterSelectWindow; // 임시 

        [SerializeField] TextMeshProUGUI characterNameText;
        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI skillDescriptionText;
        [SerializeField] TextMeshProUGUI characterDescriptionText;
        [SerializeField] GameObject unselectObj;

        Action<int> OnCharacterUseEventCallBack = null;
        int currSelectedCharacterID = -1;

        public void Open(int selectedCharacterID, Action<int> onCharacterUseEventCallBack)
        {
            JCharacterData characterData = JsonDataManager.Instance.GetSingleData<JCharacterData>(selectedCharacterID, E_JSON_TYPE.JCharacterData);

            if(characterData == null)
            {
                Debug.LogError("characterData is Null!!");
                return;
            }

            OnCharacterUseEventCallBack = onCharacterUseEventCallBack;

            currSelectedCharacterID = characterData.ID;
            characterNameText.text = characterData.Name;

            // 캐릭터 이미지 세팅

            // 스킬 설명 세팅

            // 캐릭터 설명 세팅

            // 유저데이터 나오면 변경해야 함
            unselectObj.SetActive(characterSelectWindow.CurrUseCharacterID == currSelectedCharacterID);

            this.gameObject.SetActive(true);
        }
        
        public void Close()
        {
            this.gameObject.SetActive(false);
        }

        public void OnClickUseCharacter()
        {
            if (characterSelectWindow.CurrUseCharacterID == currSelectedCharacterID)
                return;

            // 유저데이터 나오면 변경해야 함
            OnCharacterUseEventCallBack?.Invoke(currSelectedCharacterID);

            unselectObj.SetActive(characterSelectWindow.CurrUseCharacterID == currSelectedCharacterID);
        }
    }

}
