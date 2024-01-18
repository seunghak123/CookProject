using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScenarioChapterItem : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<ScenarioChapterData>
{
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private Image chapterImage;
    [SerializeField] private TextMeshProUGUI chapterStarStateText;

    JChapterData chapterData;

    public void OnPostSetupItems()
    {
        Button button = this.gameObject.GetOrAddComponent<Button>();
        button.transition = Selectable.Transition.None;
        button.onClick.AddListener(() => OnClickEvent());
    }

    public void OnUpdateItem(GameObject obj, ScenarioChapterData infos)
    {
        SetChapterData(infos.data);
    }

    public void SetChapterData(JChapterData chapterData)
    {
        if (chapterData == null)
        {
            Debug.LogError("chapterData is Null");
            return;
        }

        this.chapterData = chapterData;
        chapterNameText.text = chapterData.Name;

        // 챕터에 따른 이미지 세팅이 필요

        // 클리어 상태에 따른 별 세팅이 필요
        chapterStarStateText.text = "0/15";
    }

    public void OnClickEvent()
    {
        if(chapterData == null)
        {
            Debug.LogError("chapterData is Null");
            return;
        }

        EnterScenarioStageSelectWindow();
    }

    public void EnterScenarioStageSelectWindow()
    {
        ScenarioStageSelectWindow stageSelectWindow = (ScenarioStageSelectWindow)UIManager.Instance.PushUI(UI_TYPE.ScenarioStageSelectWindow);
        stageSelectWindow.Init(chapterData);
    }
}
