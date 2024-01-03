using Seunghak.Common;
using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class StageWindow : BaseUIWindow
    {
        [SerializeField] CurrentStageInfo currentStageInfo;
        [SerializeField] StageListInfo stageListInfo;

        JStageData firstStageData; // 현재 챕터의 첫번째 데이터
        
        public void SetStageInfo(JNationData scnarioChapterData)
        {
            string key = (scnarioChapterData.ID * 5 - 4).ToString(); // 불안하긴 함 (임시)
            firstStageData = JsonDataManager.LoadJsonDatas<JStageData>(E_JSON_TYPE.JStageData)[key];

            stageListInfo.SetStageListInfo(CallBack, firstStageData);
        }

        public void CallBack(int id)
        {
            // 임시임시~
            currentStageInfo.SetCurrntStageInfo(id);
        }

        public override void EnterWindow()
        {
            base.EnterWindow();
        }

        public override void StartWindow()
        {
            base.StartWindow();
        }

        public override void ExitWindow()
        {
            base.ExitWindow();
        }

        #region OnClick Event
        public void EnterLobbyWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.LobbyWindow);
        }

        public void EnterScenarioWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameScenarioWindow);
        }

        public void OnClickPlayStage()
        {
            // 플레이 할 수 있는 스테이지인지 확인하고 가능하다면, 실행
        }

        public void OnClickNextStateInfo()
        {

        }

        public void OnCilckPrevStateInfo()
        {

        }
        #endregion

        public override void RestoreWindow()
        {
            base.RestoreWindow();
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