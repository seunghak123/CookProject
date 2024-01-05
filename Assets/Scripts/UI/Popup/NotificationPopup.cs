using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class NotificationPopup : BaseUIPopup
    {
        // 강제적으로 알림창을 키기 위해서는 텍스트 내의 알림내용을 받게 하고 싶은데 음...ㅠ

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
    
