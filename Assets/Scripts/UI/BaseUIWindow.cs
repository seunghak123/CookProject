using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class BaseUIWindow : BaseUI
    {
        protected bool IsActiveTopUI
        { set { UIManager.Instance.SetTopUI(ui_Type, value); } }

        public override void EnterWindow()
        {
            base.EnterWindow();
            UIManager.Instance.SetTopUI(ui_Type, false);
        }
    }
}