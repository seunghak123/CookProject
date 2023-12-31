using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Seunghak.UIManager
{
    /// <summary>
    /// 시나리오 하나하나에 대한 데이터
    /// </summary>
    public class ScenarioData
    {
        public int scenarioDataID;

    }

    public class GameScenarioWindow : BaseUIWindow
    {
       [SerializeField] ScenarioScrollView scenarioScrollView;

        public override void EnterWindow()
        {
            base.EnterWindow();

            // 데이터를 가져오고 스크롤뷰에 넣어주어 세팅을 합시다.

            scenarioScrollView.Init(); // 각 시나리오에 대해 세팅을 해야 할듯?
            // 각 시나리오에 대한 세팅이 필요하며, 최대 몇개가 보이는지 확인하시고,
            // 각 데이터에 대한 세팅을 끝내야 함 원소를 두고 하면 될 거 같은데...?
        }

        public void EnterGameModeWindow()
        {

        }

        #region OnClick Event
        public void EnterStageWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.StageWindow);
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

