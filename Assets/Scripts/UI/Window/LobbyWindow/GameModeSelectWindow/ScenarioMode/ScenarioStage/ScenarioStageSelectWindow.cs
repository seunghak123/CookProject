using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ScenarioStageSelectWindow : BaseUIWindow
    {
        [SerializeField] SelectedStageInfoArea selectedStageInfoArea;
        [SerializeField] ScenarioStageGroup selectedStageGroup;

        [SerializeField] Button prevStageButton;
        [SerializeField] Button nextStageButton;
        [SerializeField] Button gamePlayButton;

        JStageData[] stageDatas = new JStageData[5];
        JChapterData chapterData;

        public void Init(int chapterDataID)
        {
            SetChapterInfo(chapterDataID);        }

        public void SetChapterInfo(int chapterDataID)
        {
            chapterData = JsonDataManager.Instance.GetChapterData(chapterDataID);

            if(chapterData == null)
            {
                Debug.LogError("chapterData Is Null!!");
                return;
            }

            // 첫번째 스테이지 데이터 수식(?)
            int firstStageDataID = (chapterData.ID - 1) * 5 + 1;

            // 스테이지 데이터 받아오기
            for (int i = 0; i < stageDatas.Length; i++)
            {
                stageDatas[i] = JsonDataManager.Instance.GetStageData(firstStageDataID + i);
            }

            // 현재 챕터의 스테이지 중에 플레이할 수 있는 최고 난이도로 스테이지를 세팅해야 함
            SetStageInfo(stageDatas[stageDatas.Length - 1].ID);

            // 스테이지 세팅
            selectedStageGroup.Init(chapterDataID, SetStageInfo);

            // 챕터 이동 버튼 활성화 여부 체크
            prevStageButton.gameObject.SetActive(chapterData.ID > 1);
            nextStageButton.gameObject.SetActive(JsonDataManager.Instance.GetChapterData(chapterData.ID + 1) != null);
        }

        public void SetStageInfo(int stageDataID)
        {
            selectedStageInfoArea.SetStageInfo(stageDataID);
        }

        public void EnterGameModeSelectWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameModeSelectWindow);
        }

        public void OnClickPrevStage()
        {
            SetChapterInfo(chapterData.ID - 1);
        }
        public void OnClickNextStage()
        {
            SetChapterInfo(chapterData.ID + 1);
        }

        public override void EnterWindow()
        {
            base.EnterWindow();
        }

        public override void ExitWindow()
        {
            base.ExitWindow();
        }

        public override void RestoreWindow()
        {
            base.RestoreWindow();
        }

        public override void StartWindow()
        {
            base.StartWindow();
        }
        public override void RegistEvent()
        {
            base.RegistEvent();
        }
        public override void DeleteRegistedEvent()
        {
            base.DeleteRegistedEvent();
        }
    }
}
