using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seunghak.UIManager
{
    public class ScenarioScrollView : OHScrollView
    {
        List<ScenarioElement> scenarioElementList = new List<ScenarioElement>();

        public void Init()
        {
            // 각 시나리오 진행도를 보고, 현재 진행 중인 스테이지를 가운데쪽으로 두고 세팅

            // 만약, 진행 중인 스테이지가 양쪽 끝에 위치할 경우에 대한 예외처리가 필요할 것
            
        }


    }

}
