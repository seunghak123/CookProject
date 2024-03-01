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

        // 일단 임시로 박아놓고 나중에 필요성이 느껴지면 런타임에 불러올 예정
        [SerializeField] Sprite clearStarSprite;
        [SerializeField] Sprite unclearStarSprite;

        JStageData jsonStageData;
        ScenarioStageResultInfo userStageData;

        public void SetStageInfo(int stageDataID)
        {
            jsonStageData = JsonDataManager.Instance.GetStageData(stageDataID);
            userStageData = UserDataManager.Instance.GetScenarioStageResultData(stageDataID);

            int maxScore = 0;

            if(userStageData != null)
                maxScore = userStageData.maxScore;

            if(jsonStageData == null )
            {
                Debug.LogError("jsonStageData Is Null!!");
                return;
            }

            stageNameText.text = jsonStageData.Name;

            // 스테이지 이미지 세팅 해야 함 (챕터 이미지랑 같아지면 따로 메서드 분리예정)

            // 클리어 별에 따른 세팅 해야 함
            clearStarImages[0].sprite = jsonStageData.score1 <= maxScore ? clearStarSprite : unclearStarSprite;
            clearStarImages[1].sprite = jsonStageData.score2 <= maxScore ? clearStarSprite : unclearStarSprite;
            clearStarImages[2].sprite = jsonStageData.score3 <= maxScore ? clearStarSprite : unclearStarSprite;

            // 클리어 조건 점수 세팅
            clearConditionTexts[0].text = jsonStageData.score1.ToString();
            clearConditionTexts[1].text = jsonStageData.score2.ToString();
            clearConditionTexts[2].text = jsonStageData.score3.ToString();
        }
    }
}  
