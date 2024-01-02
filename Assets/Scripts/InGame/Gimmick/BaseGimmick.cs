using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGimmick : MonoBehaviour
{
    [SerializeField] private List<BaseGimmick> interactGimmicks = new List<BaseGimmick>();

    protected IEnumerator DoWork()
    {
        yield break;
    }
}
