using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Seunghak.Common;

public class StoryIngameRecipePopup : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button preButton;

    [SerializeField] private Button exitButton;
    private Action popupCloseAction = null;

    private void Awake()
    {
        nextButton.onClick.AddListener(() => { });
        preButton.onClick.AddListener(() => { });

        exitButton.onClick.AddListener(() =>
        {
            if(popupCloseAction!=null)
            {
                popupCloseAction();
            }
            GameResourceManager.Instance.DestroyObject(this.gameObject);
        }
        );
    }
    private void OnDisable()
    {
        nextButton.onClick.RemoveAllListeners();
        preButton.onClick.RemoveAllListeners();

        exitButton.onClick.RemoveAllListeners();
    }
    public void SetData(List<int> recipeIds, Action closeAction)
    {

        popupCloseAction = closeAction;
    }

    private void SetUIInfo(int recipeId)
    {

    }
}
