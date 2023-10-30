using Seunghak.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    [Header("UnitInfo")]
    [SerializeField] protected E_INGAME_AI_TYPE unitAIType = E_INGAME_AI_TYPE.NONE;
    [SerializeField] private Vector3 unitDirect = new Vector3();
    [SerializeField] private bl_Joystick joyStick = null;
    [Space(2)]
    [Header("UnitAnimation")]
    [SerializeField] private Animator unitAnim;
    [SerializeField] private UnitStructure unitInfo;

    [Space(2)]
    [Header("UnitFuction")]
    [SerializeField] private CharacterController characterController;
    private Dictionary<E_INGAME_AI_TYPE, Action> userActionDic = new Dictionary<E_INGAME_AI_TYPE, Action>();
    
    private JUnitData aiUnitData = null;
    private Action currentUnitEvent = null;

    private BaseAI targetAI = null;
    private bool isGround = true;
    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        unitAnim = GetComponentInChildren<Animator>();
        RegistAction();
    }
    private void RegistAction()
    {
        userActionDic[E_INGAME_AI_TYPE.UNIT_IDLE] = UnitIdle;
        userActionDic[E_INGAME_AI_TYPE.UNIT_MOVE] = UnitMove;
        userActionDic[E_INGAME_AI_TYPE.UNIT_EVENT] = UnitEvent;

    }
    protected void ChangeAI(E_INGAME_AI_TYPE nextAIType)
    {
        if (!unitAIType.Equals(nextAIType))
        {
            unitAIType = nextAIType;

            if (unitAnim != null)
            {
                switch (unitAIType)
                {
                    case E_INGAME_AI_TYPE.UNIT_INTERACTION:
                        unitAnim.SetTrigger("InterAct");
                        break;
                    case E_INGAME_AI_TYPE.UNIT_EVENT:
                        //unitAnim.SetTrigger("Event");
                        break;
                    case E_INGAME_AI_TYPE.UNIT_MOVE:
                        //unitAnim.SetFloat()
                        break;
                }
            }
        }
    }
    #region UnitFunction
    private void InterActObject()
    {
        //상호 작용 
    }
    #endregion UnitFunction

    #region UnitState
    protected virtual void UnitJump()
    {
        
    }
    protected virtual void UnitEvent()
    {
        if (currentUnitEvent != null)
        {
            currentUnitEvent();
        }
    }
    protected virtual void UnitMove()
    {
        Vector3 moveDirect = GetMoveDirect();

        if(moveDirect == Vector3.zero)
        {
            unitAnim.SetFloat("Speed", 0);
            ChangeAI(E_INGAME_AI_TYPE.UNIT_IDLE);
            return;
        }
        unitAnim.SetFloat("Speed", moveDirect.magnitude);

        if (isGround)
        {
            //점프중 움직임
        }
        else
        {

        }

        if (moveDirect.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        this.transform.Translate(new Vector3(moveDirect.x,0,0) * Time.deltaTime);
        
    }
    private Vector3 GetMoveDirect()
    {
        if (joyStick != null)
        {
            Vector3 direct = new Vector3(joyStick.Horizontal, joyStick.Vertical);
            return direct.normalized;
        }
        return Vector3.zero;
    }
    protected virtual void UnitIdle()
    {
        if(GetMoveDirect()!=Vector3.zero)
        {
            ChangeAI(E_INGAME_AI_TYPE.UNIT_MOVE);
            //움직임으로 변경
        }
    }
    private void Action()
    {
        if (userActionDic.ContainsKey(unitAIType))
        {
            userActionDic[unitAIType]();
        }
        else
        {
            unitAIType = E_INGAME_AI_TYPE.UNIT_IDLE;
        }
    }
    #endregion UnitState
    public virtual void Update()
    {
        Action();

        if (isGround)
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //여기에 상호작용 하도록 둘것ㄴ
    }
}
