using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField] protected bool holdCharacter = false;
    [SerializeField] protected bool isBlockCharacter = false;
    private bool workEnd = false;
    private bool currentWork = false;

    private int foodBoxId = 1;
    public virtual void InitObject() 
    {
        //기존에는 파일 읽고, Id 세팅한다
    }
    public bool GetIsHold()
    {
        return holdCharacter;
    }
    public virtual void DoWork()
    {
        currentWork = true;

        
    }
    public int GetFoodId()
    {
        return foodBoxId;
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
