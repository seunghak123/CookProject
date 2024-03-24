using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Seunghak.SceneManager;
using Cysharp.Threading.Tasks;
using Seunghak.Common;

//EndData 상속구조 만들것
public class StoryEndData
{
    public JStageData stageData;
    public int stageScore;
    public StoryEndData(JStageData stageData,int stageScore)
    {
        this.stageData = stageData;
        this.stageScore = stageScore;
    }
}
public class StoryIngameEndUI : MonoBehaviour
{
    [Serializable]
    class GainStars
    {
        public GameObject gainStars;
        public TextMeshProUGUI gainStarScores;
    }
    [Header("Ingame Character")]
    [SerializeField] private Transform spawnCharacterPos;

    [Header("Ingame Logic Buttons")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextStageButton;
    [Header("Ingame Gain Things")]
    [SerializeField] private GainStars[] gainStars;
    [SerializeField] private TextMeshProUGUI gainScores;
    [SerializeField] private TextMeshProUGUI gainCoins;

    private void OnEnable()
    {
        homeButton.onClick.AddListener(OnClickHomeButton);
        retryButton.onClick.AddListener(OnClickRetryButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
    }
    private void OnDisable()
    {
        homeButton.onClick.RemoveListener(OnClickHomeButton);
        retryButton.onClick.RemoveListener(OnClickRetryButton);
        nextStageButton.onClick.RemoveListener(OnClickNextStageButton);
    }

    private void OnClickHomeButton()
    {
        SceneManager.Instance.CurDeliverData = new LobbySceneData(false);
        SceneManager.Instance.ChangeScene(E_SCENE_TYPE.LOBBY);
    }
    private void OnClickRetryButton()
    {
        //IngameSceneData ingameData = SceneManager.Instance.CurDeliverData as IngameSceneData;

        //ingameData.stageId += 1;

        SceneManager.Instance.ChangeScene(E_SCENE_TYPE.INGAME);
    }
    private void OnClickNextStageButton()
    {
        IngameSceneData ingameData = SceneManager.Instance.CurDeliverData as IngameSceneData;

        ingameData.stageId += 1;

        SceneManager.Instance.ChangeScene(E_SCENE_TYPE.INGAME);
    }

    public void SetEndInfo(StoryEndData stageInfo)
    {
        //여기에 스토리 끝 처리
        UserDataManager.Instance.SetUserStoryInfo(stageInfo);
    }
    public async UniTask StartIngameEnd()
    {
        await UniTask.NextFrame();
    }
}
