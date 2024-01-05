using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using Seunghak.Common;

namespace Seunghak.UIManager
{
    public class SelectCharaterInfo : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI characterNameText;
        
        public void Init()
        {
            this.gameObject.SetActive(false);
        }

        public void SelectCharacterEvent(JChapterData characterData)
        {
            if(this.gameObject.activeSelf == false)
                this.gameObject.SetActive(true);

            characterNameText.text = characterData.Name;
        }
    }
}
