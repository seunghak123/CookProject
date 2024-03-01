using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Seunghak.Common;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] private string convertStringEnum;
    private TextMeshProUGUI currentText;
    
    //현재 폰트 세팅해주고, 폰트 변경시 기존 폰트 메모리에서 제거
    //private Font currentFontData;
    void Start()
    {
        currentText = GetComponent<TextMeshProUGUI>();
        if(currentText == null)
        {
            Destroy(this);
            return;
        }

        SetStringKey(convertStringEnum);
    }
    public void SetStringKey(string stringEnum)
    {
        string inputtext = "";

        //JsonDataManager.Instance.GetSingleData<d>
        //데이터 파일에서 해당 키값 찾아서 텍스트에 입력
        currentText.text = inputtext;
        //currentText.font = //
    }
    public void SetString(string stringEnum, params string[] inputdatas)
    {
        string inputtext = "";
        inputtext = string.Format(inputtext, inputdatas);
        currentText.text = inputtext;
    }
}
