using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class SelectedStageInfoArea : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI stageNameText;
        [SerializeField] Image stageImage;
        [SerializeField] Image[] clearStarImages = new Image[3];
        [SerializeField] TextMeshProUGUI[] clearConditionTexts = new TextMeshProUGUI[3];

        JStageData stageData;

        public void SetStageInfo(int stageDataID)
        {
            stageData = JsonDataManager.Instance.GetStageData(stageDataID);

            if(stageData == null )
            {
                Debug.LogError("stageData Is Null!!");
                return;
            }

            stageNameText.text = stageData.Name;

            // 스테이지 이미지 세팅 해야 함 (챕터 이미지랑 같아지면 따로 메서드 분리예정)

            // 클리어 별에 따른 세팅 해야 함

            // 클리어 조건 점수 세팅
            clearConditionTexts[0].text = stageData.score1.ToString();
            clearConditionTexts[1].text = stageData.score2.ToString();
            clearConditionTexts[2].text = stageData.score3.ToString();
        }
    }
}  
