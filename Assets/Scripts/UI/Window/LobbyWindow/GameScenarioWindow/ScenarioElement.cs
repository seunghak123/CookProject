using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

namespace Seunghak.UIManager
{
    public class ScenarioChapterElement : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<ScnarioChapterData>, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] TextMeshProUGUI chapterNameText;
        [SerializeField] Image chapterImage;
        [SerializeField] TextMeshProUGUI ClearStateInfoText;
        int currChapterID = -1;

        private bool isDrag = false;

        public void OnPostSetupItems()
        {

        }

        public void OnUpdateItem(GameObject obj, ScnarioChapterData infos)
        {
            chapterNameText.text = infos.data.Name.ToString();
            currChapterID = infos.data.ID;
            
            // 해당 시나리오 챕터의 스테이지를 확인해서 세팅해야 함 (임시)
            ClearStateInfoText.text = "0/15"; 
        }

        public void OnClickEvent()
        {
            if (isDrag) 
                return;

            // 내가 선택된 스테이지에 입장이 가능한지 체크해서 가능하면 이동
            
            // 이동
            StageWindow stageWindow = (StageWindow)UIManager.Instance.PushUI(UI_TYPE.StageWindow);
            stageWindow.Init();
            stageWindow.SetStageInfo(currChapterID);
        }

        public void OnBeginDrag(PointerEventData e)
        {
            isDrag = true;
        }

        public void OnEndDrag(PointerEventData e)
        {
            isDrag = false;
        }
    }
}
