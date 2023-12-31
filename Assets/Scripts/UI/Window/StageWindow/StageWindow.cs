using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class StageWindow : BaseUIWindow
    {
        public override void EnterWindow()
        {
            base.EnterWindow();
            

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
        #endregion

        public void SetStageInfo()
        {

        }

        public void OnClickNextStateInfo()
        {

        }

        public void OnCilckPrevStateInfo()
        {

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