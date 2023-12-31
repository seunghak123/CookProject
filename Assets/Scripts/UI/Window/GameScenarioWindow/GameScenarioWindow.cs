using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Seunghak.UIManager
{
    /// <summary>
    /// 시나리오 하나하나에 대한 데이터 ( 임시 )
    /// </summary>
    public class ScenrioData : CommonScrollItemData
    {
        public int ID;

        public ScenrioData(int ID)
        {
            this.ID = ID;
        }
    }

    public class GameScenarioWindow : BaseUIWindow
    {
        [SerializeField] ScenarioScrollView scenarioScrollView;
        [SerializeField] int maxCount; // 생성개수

        List<ScenrioData> scenrioDataList =new List<ScenrioData>();

        public override void EnterWindow()
        {
            base.EnterWindow();

            for(int i = 0; i < maxCount; i++)
            {
                scenrioDataList.Add(new ScenrioData(i));
            }
            // 데이터를 가져오고 스크롤뷰에 넣어주어 세팅을 합시다.

            scenarioScrollView.InitScrollView(scenrioDataList);

        }

        #region OnClick Event
        public void EnterGameModeWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameModeWindow);
        }
        public void EnterStageWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.StageWindow);
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

