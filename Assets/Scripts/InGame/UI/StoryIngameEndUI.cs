using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

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
}
