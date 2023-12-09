using Seunghak.Common;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    [Header("UnitInfo")]
    [SerializeField] protected E_INGAME_AI_TYPE unitAIType = E_INGAME_AI_TYPE.NONE;
    [SerializeField] private bl_Joystick joyStick = null;
    [Space(2)]
    [Header("UnitAnimation")]
    [SerializeField] private Animator unitAnim;
    [SerializeField] private SkeletonAnimation unitSpineAnim;
    [SerializeField] private UnitStructure unitInfo;

    [Space(2)]
    [Header("UnitFuction")]
    [SerializeField, Range(1, 100)] private float characterJumpPower = 80;
    [SerializeField, Range(0.5f, 5)] private float characterSpeed = 1;
    private Rigidbody2D characterRigid;
    private Dictionary<E_INGAME_AI_TYPE, Action> userActionDic = new Dictionary<E_INGAME_AI_TYPE, Action>();
    
    private Action currentUnitEvent = null;
    private BaseObject targetObject = null;
    private BasicMaterialData handleObjectData = null;
    public BasicMaterialData HandleObjectData
    {
        set { handleObjectData = value; }
        get { return handleObjectData; }
    }
    private int currentFood = 0;
    private bool isGround = true;
    public bool IsGround
    {
        get { return isGround; }
        set
        {
            isGround = value;
            if (unitAnim != null)
            {
                unitAnim.SetBool("IsGround", isGround);
            }
        }
    }
    public void Awake()
    {
        characterRigid = GetComponent<Rigidbody2D>();
        unitAnim = GetComponentInChildren<Animator>();
        RegistAction();
    }
    private void RegistAction()
    {
        userActionDic[E_INGAME_AI_TYPE.UNIT_IDLE] = UnitIdle;
        userActionDic[E_INGAME_AI_TYPE.UNIT_MOVE] = UnitMove;
        userActionDic[E_INGAME_AI_TYPE.UNIT_EVENT] = UnitEvent;
        userActionDic[E_INGAME_AI_TYPE.UNIT_INTERACTION] = UnitInterAct;
    }
    public void SetAnimTrigger(string triggerName)
    {
        if (unitAnim != null)
        {
            unitAnim.SetTrigger(triggerName);
        }
    }
    public void DoAction(E_INGAME_AI_TYPE actionType)
    {
        ChangeAI(actionType);
    }
    protected void ChangeAI(E_INGAME_AI_TYPE nextAIType)
    {
        //if (!unitAIType.Equals(nextAIType))
        {
            unitAIType = nextAIType;

            if (unitAnim != null)
            {
                switch (unitAIType)
                {
                    case E_INGAME_AI_TYPE.UNIT_INTERACTION:
                        InterActObject();
                        break;
                    case E_INGAME_AI_TYPE.UNIT_EVENT:
                        unitAnim.SetTrigger("Event");
                        break;
                }
            }
        }
    }
    #region UnitFunction

    private void UnitInterAct()
    {
        if(targetObject!=null&& targetObject.GetIsHold())
        {
            //여기서 멈춰있고,
        }
        else
        {



            Vector3 moveDirect = GetMoveDirect();

            if(moveDirect!=Vector3.zero)
            {
                ChangeAI(E_INGAME_AI_TYPE.UNIT_MOVE);
            }
        }
    }
    private void InterActObject()
    {

        if (targetObject ==null)
        {
            if (HandleObjectData != null)
            {
                ThrowHandlingObject();
            }

            ChangeAI(E_INGAME_AI_TYPE.UNIT_IDLE);
            return;
        }

        if (targetObject.IsWork())
        {
            //현재 일하고 있는중이라 접근 불가
        }
        else
        {
            targetObject.DoWork(this, HandleObjectData);
        }
    }
    private void ThrowHandlingObject()
    {
        GameObject spawnObject = GameResourceManager.Instance.SpawnObject("DroppedObject");

        if (spawnObject == null)
        {
            Debug.Log("Something Problem");

            return;
        }
        spawnObject.transform.position = this.transform.position + Vector3.up;

        DroppedObject dropObject = spawnObject.GetComponent<DroppedObject>();

        if(dropObject == null)
        {
            return;
        }

        dropObject.SetObjectInfo(0);
        handleObjectData = null;

        Vector3 throwDirect = new Vector3(this.transform.localScale.x, 1, 0);
        Rigidbody2D dropRigid = dropObject.GetComponent<Rigidbody2D>();
        if(dropRigid!=null)
        {
            dropRigid.AddForce(throwDirect * dropObject.GetObjectThrowPower() * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    #endregion UnitFunction
    #region UnitState
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
            //unitSpineAnim.AnimationState.SetAnimation(0, "Idle", true);
            unitAnim.SetFloat("Speed", 0);
            IsGround = true;
            ChangeAI(E_INGAME_AI_TYPE.UNIT_IDLE);
            return;
        }
        unitAnim.SetFloat("Speed", moveDirect.magnitude);

        if (moveDirect.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        this.transform.Translate(new Vector3(moveDirect.x,0,0) * Time.deltaTime * characterSpeed);

        float xValue = Mathf.Abs(moveDirect.x);
        float yValue = moveDirect.y;

        if (xValue < yValue)
        {
            if (characterRigid == null)
            {
                return;
            }

            if (IsGround)
            {
                unitAnim.SetTrigger("Jump");
                characterRigid.AddForce(Vector2.up * characterJumpPower * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        //현재 점프중
        Vector3 gravityVector = characterRigid.velocity;

        unitAnim.SetFloat("YSpeed", gravityVector.y);
    }
    private Vector3 GetMoveDirect()
    {

        //Input받기
        if (joyStick != null)
        {
            Vector3 direct = new Vector3(joyStick.Horizontal, joyStick.Vertical);
            return direct.normalized;
        }
        return Vector3.zero;
    }
    protected virtual void UnitIdle()
    {
        unitAnim.SetFloat("Speed", 0);
        if (GetMoveDirect()!=Vector3.zero)
        {
            ChangeAI(E_INGAME_AI_TYPE.UNIT_MOVE);
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
    RaycastHit2D hitresult;
    public virtual void FixedUpdate()
    {
        hitresult = Physics2D.Raycast(this.transform.position, Vector3.down, 0.15f, 1 << 9);
        if (hitresult.collider == null)
        {
            IsGround = false;
        }
        else
        {
            IsGround = true;
        }

        Action();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(targetObject==null)
        {
            if (collision != null && collision.tag == "InterActObject")
            {
                targetObject = collision.GetComponent<BaseObject>();

                //currentFood = targetObject
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetObject!=null && targetObject.gameObject == collision.gameObject)
        {
           targetObject = null;
        }

    }
}
