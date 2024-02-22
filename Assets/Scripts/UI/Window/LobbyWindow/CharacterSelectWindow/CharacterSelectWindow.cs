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

        public int CurrUseCharacterID { get; private set; } = -1;
        public int CurrSelectCharacterID { get; private set; } = -1;

        public void Init()
        {
            CurrUseCharacterID = 1; // 현재 사용 중인 캐릭터 ID를 가져와야 함
            CurrSelectCharacterID = CurrUseCharacterID;

            selectedCharacterInfo.SetCharacterInfo(CurrSelectCharacterID, CharacterUseEventCallBack);
            scrollView.InitScrollView(JsonDataManager.LoadJsonDatas<JCharacterData>(E_JSON_TYPE.JCharacterData).Values.ToList());
        }
        public void CharacterSelectEventCallBack(int characterDataID)
        {
            this.CurrSelectCharacterID = characterDataID;

            selectedCharacterInfo.SetCharacterInfo(CurrSelectCharacterID, CharacterUseEventCallBack);
            scrollView.UpdateScrollViewInfo();
        }
        public void CharacterUseEventCallBack(int characteerDataID)
        {
            this.CurrUseCharacterID = characteerDataID;
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

