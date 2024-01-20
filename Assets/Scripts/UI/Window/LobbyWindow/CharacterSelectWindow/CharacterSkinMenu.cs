using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class CharacterSkinMenu : MonoBehaviour
    {
        public void Open(int selectedCharacterID)
        {
            JCharacterData characterData = JsonDataManager.Instance.GetSingleData<JCharacterData>(selectedCharacterID, E_JSON_TYPE.JCharacterData);

            if (characterData == null)
            {
                Debug.LogError("characterData is Null!!");
                return;
            }

            // 스킨 세팅

            this.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }

}
