using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
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
