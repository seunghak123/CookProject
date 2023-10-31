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
    [SerializeField, Range(1, 100)] private float characterJumpPower; 
    private Rigidbody2D characterRigid;
    private Dictionary<E_INGAME_AI_TYPE, Action> userActionDic = new Dictionary<E_INGAME_AI_TYPE, Action>();
    
    private Action currentUnitEvent = null;
    private BaseObject targetObject = null;
    private bool isGround = true;
    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        characterRigid = GetComponent<Rigidbody2D>();
        unitAnim = GetComponentInChildren<Animator>();
        RegistAction();
    }
    private void RegistAction()
    {
        userActionDic[E_INGAME_AI_TYPE.UNIT_IDLE] = UnitIdle;
        userActionDic[E_INGAME_AI_TYPE.UNIT_MOVE] = UnitMove;
        userActionDic[E_INGAME_AI_TYPE.UNIT_EVENT] = UnitEvent;
        userActionDic[E_INGAME_AI_TYPE.UNIT_JUMP] = UnitJump;
        userActionDic[E_INGAME_AI_TYPE.UNIT_INTERACTION] = InterActObject;
    }
    public void DoAction(E_INGAME_AI_TYPE actionType)
    {
        ChangeAI(actionType);
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
                    case E_INGAME_AI_TYPE.UNIT_JUMP:
                        Jump();
                        break;
                }
            }
        }
    }
    #region UnitFunction
    private void Jump()
    {
        if (characterRigid == null)
        {
            return;
        }
        characterRigid.AddForce(Vector2.up* characterJumpPower, ForceMode2D.Impulse);
    }
    private void InterActObject()
    {
        if (targetObject != null)
        {

        }
        //상호 작용 
    }
    #endregion UnitFunction

    #region UnitState
    protected virtual void UnitJump()
    {
        Vector3 moveDirect = GetMoveDirect();

        if (moveDirect == Vector3.zero)
        {
            isGround = true;
            ChangeAI(E_INGAME_AI_TYPE.UNIT_IDLE);
        }
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

        Debug.DrawLine(this.transform.position, this.transform.position- Vector3.down/10,Color.red,0.1f);
        if (Physics2D.Raycast(this.transform.position, Vector3.down, 0.15f, 9))
        {
            isGround = false;

        }

        if (isGround)
        {
            ChangeAI(E_INGAME_AI_TYPE.UNIT_JUMP);
            //점프중 움직임
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
        if(collision!=null && collision.tag == "InterActObject")
        {
            targetObject = collision.GetComponent<BaseObject>();
        }
        //여기에 상호작용 하도록 둘것ㄴ
    }
}
