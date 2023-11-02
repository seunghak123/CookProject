using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField] private bool holdCharacter = false;
    private bool workEnd = false;
    private bool currentWork = false;
    public bool GetIsHold()
    {
        return holdCharacter;
    }
    public virtual void DoWork()
    {
        currentWork = true;

        
    }
    public bool IsWork()
    {
        return currentWork;
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
