using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class CharacterSelectWindow : BaseUIWindow
    {
        [SerializeField] CharacterSelectScrollView scrollView;
        [SerializeField] SelectedCharacterInfo selectedCharacterInfo;

        public int currUseCharacterID { get; private set; } = -1;
        public int currSelectCharacterID { get; private set; } = -1;

        public void Init()
        {
            currUseCharacterID = 1; // 현재 사용 중인 캐릭터 ID를 가져와야 함
            currSelectCharacterID = currUseCharacterID;

            selectedCharacterInfo.SetCharacterInfo(currSelectCharacterID);
            scrollView.InitScrollView(JsonDataManager.LoadJsonDatas<JCharacterData>(E_JSON_TYPE.JCharacterData).Values.ToList());
        }
        public void CharacterSelectEventCallBack(int characterDataID)
        {
            this.currSelectCharacterID = characterDataID;

            selectedCharacterInfo.SetCharacterInfo(currSelectCharacterID);
            scrollView.UpdateScrollViewInfo();
        }

        public override void EnterWindow()
        {
            base.EnterWindow();

            Init();
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

