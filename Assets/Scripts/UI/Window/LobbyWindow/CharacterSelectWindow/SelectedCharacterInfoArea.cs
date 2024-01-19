using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class SelectedCharacterInfoArea : MonoBehaviour
    {
        public int currSelectedCharacterID { get; private set; } = -1;

        public void SetCharacterInfo(int selectedCharacterID)
        {
            // 귀찮다.......
        }
    }
}
