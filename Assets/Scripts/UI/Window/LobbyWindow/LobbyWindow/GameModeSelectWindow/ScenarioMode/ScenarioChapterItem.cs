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
    [SerializeField] private TextMeshProUGUI shopItemlbl;

    public void OnPostSetupItems()
    {

    }

    public void OnUpdateItem(GameObject obj, ScenarioChapterData infos)
    {

    }

}
