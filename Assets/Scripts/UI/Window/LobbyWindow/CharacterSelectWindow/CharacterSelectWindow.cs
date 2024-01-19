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
        [SerializeField] SelectedCharacterInfoArea characterInfoArea;

        public void Init()
        {

        }

        public void CharacterSelectEventCallBack(int characterDataID)
        {
            // 캐릭터
        }

        public override void EnterWindow()
        {
            base.EnterWindow();

            scrollView.InitScrollView(JsonDataManager.LoadJsonDatas<JCharacterData>(E_JSON_TYPE.JCharacterData).Values.ToList());
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

