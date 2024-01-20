using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class SelectedCharacterInfo : MonoBehaviour
    {
        [SerializeField] CharacterInfoMenu characterInfoMenu;
        [SerializeField] CharacterSkinMenu characterSkinMenu;

        public int currSelectedCharacterID { get; private set; } = -1;

        public void SetCharacterInfo(int selectedCharacterID)
        {
            currSelectedCharacterID = selectedCharacterID;

            characterInfoMenu.Open(currSelectedCharacterID);
            characterSkinMenu.Close();
        }

        public void OnClickCharacterInfo()
        {
            if(currSelectedCharacterID == -1)
                return;

            characterInfoMenu.Open(currSelectedCharacterID);
            characterSkinMenu.Close();
        }

        public void OnClickCharacterSkin()
        {
            if (currSelectedCharacterID == -1)
                return;

            characterSkinMenu.Open(currSelectedCharacterID);
            characterInfoMenu.Close();
        }
    }
}
