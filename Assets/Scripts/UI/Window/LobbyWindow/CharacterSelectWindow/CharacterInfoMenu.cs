using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Seunghak.Common;

namespace Seunghak.UIManager
{
    public class CharacterInfoMenu : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI characterNameText;
        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI skillDescriptionText;
        [SerializeField] TextMeshProUGUI characterDescriptionText;
        [SerializeField] GameObject unselectObj;

        public void Open(int selectedCharacterID)
        {
            JCharacterData characterData = JsonDataManager.Instance.GetSingleData<JCharacterData>(selectedCharacterID, E_JSON_TYPE.JCharacterData);

            if(characterData == null)
            {
                Debug.LogError("characterData is Null!!");
                return;
            }

            characterNameText.text = characterData.Name;
            
            // 캐릭터 이미지 세팅

            // 스킬 설명 세팅

            // 캐릭터 설명 세팅

            this.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }

        public void OnClickUseCharacter()
        {

        }
    }

}
