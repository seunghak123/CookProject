using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seunghak.UIManager
{
    public class SelectedStageInfo : MonoBehaviour
    {
        [SerializeField] Image stageImage; // 썸네일(?)
        [SerializeField] Image[] clearPointImage = new Image[3]; // 클리어한 별(?)
        

        public void Init()
        {
            // 스테이지 정보를 받아와서 최초 세팅
        }

        public void SetStageInfo()
        {

            // 이벤트에 의해 갱신될 수 있는 데이터 세팅
        }
    }
}

