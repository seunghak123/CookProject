using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class StageListInfo : MonoBehaviour
    {
        [SerializeField] StageElement sampleStageElement;
        List<StageElement> stageElementList = new List<StageElement>();

        public void SetStageListInfo(Action<int> OnStageClickEvent, List<JStageData> stageDataList)
        {
            sampleStageElement.gameObject.SetActive(false);
           
            while(stageElementList.Count < stageDataList.Count)
            {
                GameObject g = GameObject.Instantiate(sampleStageElement.gameObject, this.transform);
                stageElementList.Add(g.GetComponent<StageElement>());
            }
            
            for (int i = 0; i < stageDataList.Count; i++)
            {
                stageElementList[i].SetStageInfo(OnStageClickEvent, stageDataList[i].ID);
                stageElementList[i].gameObject.SetActive(true);
            }

            // 역순으로 확인해서 도전 가능한 마지막 스테이지를 세팅
            for(int i = stageElementList.Count - 1; i >= 0; i--)
            {
                if (stageElementList[i].IsLock == false || i == 0)
                {
                    stageElementList[i].OnClickEvent();
                    break;
                }
            }
        }
    }
}
