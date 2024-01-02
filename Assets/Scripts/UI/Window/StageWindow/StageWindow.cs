using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class StageWindow : BaseUIWindow
    {
        [SerializeField] SelectedStageInfo selectedStageInfo;
        

        public override void EnterWindow()
        {
            base.EnterWindow();

            // 스크롤뷰 관리를 여기서 할지 SelectedStageInfo에서 할지 고민

            // 진행 중인 스테이지에 관련된 데이터 받아오기

            // 최초에 한번만 세팅될 스테이지 데이터 세팅
            selectedStageInfo?.Init(); // 매개변수 전달 예정
            

        }

        public void SetStageInfo()
        {

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
            ExitWindow();
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