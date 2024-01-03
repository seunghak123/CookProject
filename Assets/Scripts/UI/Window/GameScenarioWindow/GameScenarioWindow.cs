using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Seunghak.UIManager
{
    public class ScnarioChapterData : CommonScrollItemData
    {
        public JNationData data;

        public ScnarioChapterData(JNationData data)
        {
            this.data = data;
        }
    }
    
    public class GameScenarioWindow : BaseUIWindow
    {
        [SerializeField] int maxCount; // 생성개수
        [SerializeField] OHScrollView scenarioScrollView;
        List<ScnarioChapterData> scnarioChapterDataList = new List<ScnarioChapterData>();

        public override void EnterWindow()
        {
            base.EnterWindow();

            // Nation 데이터를 가져와서 세팅

            List<JNationData> nationDataList = JsonDataManager.LoadJsonDatas<JNationData>(E_JSON_TYPE.JNationData).Values.ToList();

            for (int i = 0; i < nationDataList.Count; i++)
                scnarioChapterDataList.Add(new ScnarioChapterData(nationDataList[i]));

            // 데이터를 가져오고 스크롤뷰에 넣어주어 세팅
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

