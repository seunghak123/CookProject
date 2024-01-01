using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Seunghak.UIManager
{
    public class ScenarioElement : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<ScenarioData>
    {
        [SerializeField] private TextMeshProUGUI testText;

        public void Init()
        {

        }

        public void OnPostSetupItems()
        {
            
        }

        public void OnUpdateItem(GameObject obj, ScenarioData infos)
        {
            // 데이터 세팅 테스트
            testText.text = infos.ID.ToString();
        }

    }
}
