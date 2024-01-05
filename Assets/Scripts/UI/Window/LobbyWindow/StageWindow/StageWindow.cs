using Seunghak.Common;
using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class StageWindow : BaseUIWindow
    {
        [SerializeField] CurrentStageInfo currentStageInfo;
        [SerializeField] StageListInfo stageListInfo;

        Dictionary<int, JStageData> stageDataDict = new Dictionary<int, JStageData>();
        bool isInit = false;
        int currChapterID;

        public void Init()
        {
            if (isInit)
                return;

            isInit = true;

            List<JStageData> list = JsonDataManager.LoadJsonDatas<JStageData>(E_JSON_TYPE.JStageData).Values.ToList();
            foreach(JStageData data in list)
                stageDataDict.Add(data.ID, data);
        }

        public void SetStageInfo(int chapterID)
        {
            currChapterID = chapterID;
            List<JStageData> tempStageList = new List<JStageData>();
            for(int i = 0; i < 5; i++)
            {
                int key = chapterID * 5 - 4 + i;
                if (stageDataDict.ContainsKey(key))
                    tempStageList.Add(stageDataDict[key]);
            }

            stageListInfo.SetStageListInfo(StageClickEventCallBack, tempStageList);
        }

        public void StageClickEventCallBack(int id)
        {
            currentStageInfo.SetCurrntStageInfo(id);
        }

        public override void EnterWindow()
        {
            base.EnterWindow();
        }

        public override void StartWindow()
        {
            base.StartWindow();
        }

        public override void ExitWindow()
        {
            base.ExitWindow();
        }

        #region OnClick Event
        public void EnterLobbyWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.LobbyWindow);
        }

        public void EnterScenarioWindow()
        {
            UIManager.Instance.PushUI(UI_TYPE.GameScenarioWindow);
        }

        public void OnClickPlayStage()
        {
            // 플레이가 가능한 스테이지인지 확인하고 ㄱㄱ
        }

        public void OnClickNextStateInfo()
        {
            // 현재 스테이지리스트 중에 마지막 스테이지가 클리어되어 있다면
            // 다음 스테이지가 있는 지 확인하고, 있을 경우 스테이지 정보를 갱신
            if(stageDataDict.ContainsKey(currChapterID * 5 + 1))
            {
                SetStageInfo(currChapterID + 1);
            }
            else
            {
                Debug.Log("다음 스테이지가 없습니다");
            }
        }

        public void OnCilckPrevStateInfo()
        {
            if(stageDataDict.ContainsKey(currChapterID * 5 - 5))
            {
                SetStageInfo(currChapterID - 1);
            }
            else
            {
                Debug.Log("이전 스테이지가 없습니다");
            }
        }
        #endregion

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