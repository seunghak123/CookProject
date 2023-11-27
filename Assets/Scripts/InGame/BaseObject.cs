using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField] protected bool holdCharacter = false;
    [SerializeField] protected bool isBlockCharacter = false;
    private bool workEnd = false;
    private bool currentWork = false;
    private BaseAI currentWorker = null;
    private BasicMaterialData makedMaterial = null;
    public virtual void InitObject() 
    {
        //기존에는 파일 읽고, Id 세팅한다
    }
    public bool GetIsHold()
    {
        return holdCharacter;
    }
    public virtual void DoWork(BaseAI targetAI)
    {
        currentWork = true;
        currentWorker = targetAI; 

        //targetAI.SetAnimTrigger()
        //Invoke or Coroutine
        //상태에 따라서 달리진행
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


            BasicMaterialData makedData = null;

            if (currentWorker == null)
            {
                //currentWorker.HandleObjectData 
            }

            return true;
        }

        return false;
    }
}
