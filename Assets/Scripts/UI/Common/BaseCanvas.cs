using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class BaseCanvas : UnitySingleton<BaseCanvas>
    {
        [SerializeField] public Transform windowUIParent;
        [SerializeField] public Transform popUpUIParent;
        [SerializeField] public Transform utilUIParent;
        [SerializeField] public BaseTopUI baseTopUI;

        public void SetBaseTopUI(UI_TYPE curUIType, bool isActive = false)
        {
            if(baseTopUI != null)
            {
                baseTopUI.gameObject.SetActive(isActive);
                baseTopUI.InitTopUI(curUIType);
            }
        }
    }
}
