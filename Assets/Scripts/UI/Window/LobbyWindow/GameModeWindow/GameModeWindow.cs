using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class GameModeWindow : BaseUIWindow
    {
        public override void EnterWindow()
        {
            base.EnterWindow();

            
        }

        #region OnClick Event
        public void EnterGameScenarioWindow()
        {
            ExitWindow();
            UIManager.Instance.PushUI(UI_TYPE.GameScenarioWindow);
        }
        public void EnterLobbyWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.LobbyWindow);
        }
        #endregion

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