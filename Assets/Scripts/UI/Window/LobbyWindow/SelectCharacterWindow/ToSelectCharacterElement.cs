using Seunghak.UIManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToSelectCharacterElement : MonoBehaviour, OHScrollView.IInfiniteScrollSetup<ToSelectCharacterData>, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] SelectCharacterWindow selectCharacterWindow;
    [SerializeField] TextMeshProUGUI characterNameText;
    int currCharacterID = -1;

    private bool isDrag = false;

    public void OnPostSetupItems()
    {

    }

    public void OnClickEvent()
    {
        if (isDrag)
            return;

        // 내 캐릭터 번호를 넘겨주기
        selectCharacterWindow.SelectCharacterEvent(currCharacterID);
    }

    public void OnUpdateItem(GameObject obj, ToSelectCharacterData infos)
    {
        currCharacterID = infos.data.ID;
        characterNameText.text = infos.data.Name;
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