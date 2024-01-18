using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class ScenarioStageSelectWindow : BaseUIWindow
    {
        JStageData[] stageDatas = new JStageData[5];

        public void Init(JChapterData chapterData)
        {
            // 첫번째 스테이지 데이터 공식(?)
            int firstStageDataID = chapterData.ID * 5;

            // 스테이지 데이터 받아오기
            for (int i = 0; i < stageDatas.Length; i++)
            {
                stageDatas[i] = JsonDataManager.Instance.GetStageData(firstStageDataID + i);
            }
            

        }

        public void EnterGameModeSelectWindowButton()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameModeSelectWindow);
        }

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

