using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ScnarioChapterData : CommonScrollItemData
    {
        public JChapterData data;

        public ScnarioChapterData(JChapterData data)
        {
            this.data = data;
        }
    }
    
    public class GameScenarioWindow : BaseUIWindow
    {
        [SerializeField] OHScrollView scenarioScrollView;
        List<ScnarioChapterData> scnarioChapterDataList = new List<ScnarioChapterData>();

        public override void StartWindow()
        {
            base.StartWindow();

            List<JChapterData> nationDataList = JsonDataManager.LoadJsonDatas<JChapterData>(E_JSON_TYPE.JChapterData).Values.ToList();
            for (int i = 0; i < nationDataList.Count; i++)
                scnarioChapterDataList.Add(new ScnarioChapterData(nationDataList[i]));

            // 데이터를 가져오고 스크롤뷰에 넣어주어 세팅 (클릭 이벤트는 ScenarioElement 클래스에서 처리)
            scenarioScrollView.InitScrollView(scnarioChapterDataList);
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

