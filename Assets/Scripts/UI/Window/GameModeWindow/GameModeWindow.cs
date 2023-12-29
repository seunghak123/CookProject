using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class GameModeWindow : BaseUIWindow
    {
        public override void EnterWindow()
        {
            base.EnterWindow();

            
        }

        // OnClick Event
        public void EnterStageWindow()
        {
            ExitWindow();
            UIManager.Instance.PushUI(UI_TYPE.StageWindow);
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