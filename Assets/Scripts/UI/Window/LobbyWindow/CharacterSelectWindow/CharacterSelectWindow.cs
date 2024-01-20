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
        [SerializeField] RectTransform selectedEffectObjRectTr;

        public void Init()
        {
            scrollView.InitScrollView(JsonDataManager.LoadJsonDatas<JCharacterData>(E_JSON_TYPE.JCharacterData).Values.ToList());
            selectedEffectObjRectTr.gameObject.SetActive(false);
        }

        public void CharacterSelectEventCallBack(int characterDataID, Transform scelectedObjTr)
        {
            if(characterInfoArea.currSelectedCharacterID == characterDataID)
                return;

            // 캐릭터
            selectedEffectObjRectTr.anchoredPosition = scelectedObjTr.GetComponent<RectTransform>().anchoredPosition;
            if(selectedEffectObjRectTr.gameObject.activeSelf == false)
                selectedEffectObjRectTr.gameObject.SetActive(true);
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

