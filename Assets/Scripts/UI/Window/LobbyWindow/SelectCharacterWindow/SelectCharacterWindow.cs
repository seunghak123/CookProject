using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ToSelectCharacterData : CommonScrollItemData
    {
        public JChapterData data;

        public ToSelectCharacterData(JChapterData data)
        {
            this.data = data;
        }
    }
    
    public class SelectCharacterWindow : BaseUIWindow
    {
        [SerializeField] OHScrollView selectedCharacterScrollView;
        [SerializeField] SelectCharaterInfo selectCharaterInfo;

        Dictionary<int, ToSelectCharacterData> selectCharacterDataDict = new Dictionary<int, ToSelectCharacterData>();

        public override void StartWindow()
        {
            base.StartWindow();

            selectCharaterInfo.Init();

            // 임시 코드
            List<JChapterData> characterDataList = JsonDataManager.LoadJsonDatas<JChapterData>(E_JSON_TYPE.JCharacterData).Values.ToList();
            for (int i = 0; i < characterDataList.Count; i++)
                selectCharacterDataDict.Add(characterDataList[i].ID, new ToSelectCharacterData(characterDataList[i]));

            selectedCharacterScrollView.InitScrollView(selectCharacterDataDict.Values.ToList());
        }

        public void SelectCharacterEvent(int characterID)
        {
            if (selectCharacterDataDict.ContainsKey(characterID))
                selectCharaterInfo.SelectCharacterEvent(selectCharacterDataDict[characterID].data);
            else
                Debug.LogError($"'{characterID}'는 없는 캐릭터ID값 입니다.");
        }

        #region OnClick Event
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
