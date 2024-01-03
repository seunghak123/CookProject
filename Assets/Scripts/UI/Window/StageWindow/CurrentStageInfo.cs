using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class CurrentStageInfo : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI stageNameText;
        [SerializeField] Image stageImage; // 썸네일(?)
        [SerializeField] Image[] clearPointImage = new Image[3]; // 클리어한 별(?)

        // 일단 박아놓기 (임시)
        [SerializeField] Sprite clearSprite;
        [SerializeField] Sprite unclearSprite;

        public void SetCurrntStageInfo(int stageID)
        {
            stageNameText.text = JsonDataManager.LoadJsonDatas<JStageData>(E_JSON_TYPE.JStageData)[stageID.ToString()].Name;

        }

        public void SetStageInfo()
        {

            // 이벤트에 의해 갱신될 수 있는 데이터 세팅
        }
    }
}

