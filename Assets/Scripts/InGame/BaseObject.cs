using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    private bool workEnd = false;
    private bool currentWork = false;
    protected virtual void DoWork()
    {
        currentWork = true;

        
    }
    public virtual bool WorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;


            return true;
        }

        return false;
    }
}
