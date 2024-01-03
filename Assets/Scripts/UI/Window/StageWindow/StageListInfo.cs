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

        public void SetStageListInfo(Action<int> OnStageClickEvent, JStageData firstStageData, int stageCount = 5)
        {
            sampleStageElement.gameObject.SetActive(false);
            while(stageElementList.Count < stageCount)
            {
                GameObject g = GameObject.Instantiate(sampleStageElement.gameObject, this.transform);
                stageElementList.Add(g.GetComponent<StageElement>());
            }
            
            for (int i = 0; i < stageCount; i++)
            {
                stageElementList[i].SetStageInfo(OnStageClickEvent, firstStageData.ID + i);
                stageElementList[i].gameObject.SetActive(true);
            }
        }
    }
}
