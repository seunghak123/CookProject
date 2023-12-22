using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToolManager : MonoBehaviour
{
    IEnumerator ToolInit()
    {
        yield return new WaitForSeconds(1f);


        ApplicationManager.Instance.UserLoginSuccess();
    }
}
