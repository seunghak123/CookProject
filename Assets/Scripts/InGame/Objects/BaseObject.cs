using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    protected virtual void Awake()
    {
        // 오브젝트 초기화
        InitObject();
    }
    public virtual void InitObject()
    {

    }
    public virtual void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
    }
    public virtual void ExitWork()
    {
        //나가졌을 때 처리하는 방법
    }
    public virtual bool GetIsHold()
    {
        return true;
    }
}
