using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace Seunghak.UIManager
{
    public class SelectedCharacterInfo : MonoBehaviour
    {
        [SerializeField] CharacterInfoMenu characterInfoMenu;
        [SerializeField] CharacterSkinMenu characterSkinMenu;

        public int SelectedCharacterID { get; private set; } = -1;

        Action<int> OnCharacterUseEventCallBack = null;

        public void SetCharacterInfo(int selectedCharacterID, Action<int> onCharacterUseEventCallBack)
        {
            SelectedCharacterID = selectedCharacterID;
            OnCharacterUseEventCallBack = onCharacterUseEventCallBack;

            characterInfoMenu.Open(SelectedCharacterID, OnCharacterUseEventCallBack);
            characterSkinMenu.Close();
        }

        public void OnClickCharacterInfo()
        {
            if(SelectedCharacterID == -1)
                return;

            characterInfoMenu.Open(SelectedCharacterID, OnCharacterUseEventCallBack);
            characterSkinMenu.Close();
        }

        public void OnClickCharacterSkin()
        {
            if (SelectedCharacterID == -1)
                return;

            characterSkinMenu.Open(SelectedCharacterID);
            characterInfoMenu.Close();
        }
    }
}
