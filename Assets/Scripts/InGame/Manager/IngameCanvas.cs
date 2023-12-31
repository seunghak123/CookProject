﻿using Seunghak.SceneManager;
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

    [Header("UserUnitAction")]
    [SerializeField] private Button interActButton;

    [SerializeField] private BaseAI userAI;

    public void InitIngameCanvas()
    {

    }
    private void OnEnable()
    {
        optionButton.onClick.AddListener(OnOffOptionPanel);
        exitButton.onClick.AddListener(ExitInGame);
        interActButton.onClick.AddListener(() => {
            DoAction(E_INGAME_AI_TYPE.UNIT_INTERACTION);
        });
    }
    private void OnDisable()
    {
        optionButton.onClick.RemoveListener(OnOffOptionPanel);
        exitButton.onClick.RemoveListener(ExitInGame);
        interActButton.onClick.RemoveAllListeners();
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
    }
    private void ExitInGame()
    {
        SceneManager.Instance.ChangeScene(E_SCENE_TYPE.LOBBY);
    }
}
