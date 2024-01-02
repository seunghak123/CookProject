using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Seunghak.UIManager
{
    /// <summary>
    /// 시나리오 하나에 대한 데이터 ( 임시 )
    /// </summary>
    public class ScenarioData : CommonScrollItemData
    {
        public int ID;
        public int totalStageCount;

        public ScenarioData(int ID)
        {
            this.ID = ID;
        }
    }

    /// <summary>
    /// 시나리오 챕터 하나에 대한 데이터 ( 임시 )
    /// </summary>
    public class ScenarioChapterData
    {

    }

    public class GameScenarioWindow : BaseUIWindow
    {
        [SerializeField] int maxCount; // 생성개수
        [SerializeField] OHScrollView scenarioScrollView;
        List<ScenarioData> scenrioDataList =new List<ScenarioData>();

        public override void EnterWindow()
        {
            base.EnterWindow();

            for(int i = 0; i < maxCount; i++)
            {
                scenrioDataList.Add(new ScenarioData(i));
            }

            // 데이터를 가져오고 스크롤뷰에 넣어주어 세팅
            scenarioScrollView.InitScrollView(scenrioDataList);

        }

        #region OnClick Event
        public void EnterGameModeWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameModeWindow);
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

