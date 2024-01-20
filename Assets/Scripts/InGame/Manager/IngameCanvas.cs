using Seunghak.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameCanvas : MonoBehaviour
{
    [Header("BaseCanvasOption")]
    [SerializeField] private Button optionButton;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Transform uiSpawnPos;
    [Space(2)]
    [Header("OptionPanel")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button continueButton;

    [Header("UserUnitAction")]
    [SerializeField] private Button interActButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private BaseAI userAI;

    public void InitIngameCanvas()
    {

    }
    private void OnEnable()
    {
        optionButton.onClick.AddListener(OnOffOptionPanel);
        exitButton.onClick.AddListener(ExitInGame);
        retryButton.onClick.AddListener(RetryGame);
        continueButton.onClick.AddListener(ContinueGame);
        interActButton.onClick.AddListener(() => {
            DoAction(E_INGAME_AI_TYPE.UNIT_INTERACTION);
        });
        jumpButton.onClick.AddListener(() =>
        {
            if (userAI != null)
            {
                userAI.UnitJump();
            }
        });
    }
    private void OnDisable()
    {
        optionButton.onClick.RemoveListener(OnOffOptionPanel);
        exitButton.onClick.RemoveListener(ExitInGame);
        retryButton.onClick.RemoveListener(RetryGame);
        continueButton.onClick.RemoveListener(ContinueGame);
        interActButton.onClick.RemoveAllListeners();
        jumpButton.onClick.RemoveAllListeners();
    }
    private void DoAction(E_INGAME_AI_TYPE aiType)
    {
        if (userAI != null)
        {
            userAI.DoAction(aiType);
        }
    }
    private void OnOffOptionPanel()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);

        IngameManager.currentManager.isPause = optionPanel.activeSelf;
    }
    private void ExitInGame()
    {
        SceneManager.Instance.ChangeScene(E_SCENE_TYPE.LOBBY);
    }
    private void ContinueGame()
    {
        optionPanel.SetActive(false);
        IngameManager.currentManager.isPause = false;
    }
    private void RetryGame()
    {
        IngameManager.currentManager.ReplayStage();
        optionPanel.SetActive(false);
    }
}
