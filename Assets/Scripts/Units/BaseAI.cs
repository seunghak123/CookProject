using Seunghak.Common;
using Spine;
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

    [SerializeField] private SpriteRenderer foodHoldSpriteRender;

    [Space(2)]
    [Header("UnitFuction")]
    [SerializeField, Range(1, 500)] private float characterJumpPower = 80;
    [SerializeField, Range(0.5f, 5)] private float characterSpeed = 1;
    private Rigidbody2D characterRigid;
    private Dictionary<E_INGAME_AI_TYPE, Action> userActionDic = new Dictionary<E_INGAME_AI_TYPE, Action>();
    private Action currentUnitEvent = null;
    private BaseObject targetObject = null;
    private BasicMaterialData handleObjectData = null;
    public BasicMaterialData HandleObjectData
    {
        set 
        { 
            handleObjectData = value;
            if(handleObjectData!=null&&!handleObjectData.IsEmpty())
            {
                int foodId = handleObjectData.GetFoodId();
                JFoodObjectData foodObject = JsonDataManager.Instance.GetSingleData<JFoodObjectData>(foodId, E_JSON_TYPE.JFoodObjectData);
                foodHoldSpriteRender.sprite = SpriteManager.Instance.LoadSprite(foodObject.IconFile);
                foodHoldSpriteRender.gameObject.SetActive(true);
            }
            else
            {
                foodHoldSpriteRender.gameObject.SetActive(false);
            }
            SetUnitDefaultSpineAnimation();
        }
        get { return handleObjectData; }
    }
    private bool isGround = true;
    public bool IsGround
    {
        get { return isGround; }
        set
        {
            isGround = value;
            if (unitAnim != null)
            {
                unitAnim?.SetBool("IsGround", isGround);
            }
        }
    }
    public void Awake()
    {
        characterRigid = GetComponent<Rigidbody2D>();
        characterRigid.useFullKinematicContacts = true;
        unitAnim = GetComponentInChildren<Animator>();
        RegistAction();
    }
    public void GrabDroppedFood(int foodId)
    {
        if (HandleObjectData.IsEmpty())
        {
            BasicMaterialData makedMaterial = new BasicMaterialData();

            makedMaterial.PushMaterial(foodId);
            makedMaterial.SetFoodId(foodId);

            HandleObjectData = makedMaterial;
        }
    }
    private void RegistAction()
    {
        userActionDic[E_INGAME_AI_TYPE.UNIT_IDLE] = UnitIdle;
        userActionDic[E_INGAME_AI_TYPE.UNIT_MOVE] = UnitMove;
        userActionDic[E_INGAME_AI_TYPE.UNIT_EVENT] = UnitEvent;
        userActionDic[E_INGAME_AI_TYPE.UNIT_INTERACTION] = UnitInterAct;
    }
    public void SetAnimationWithName(string triggerName)
    {
        if (unitAnim != null)
        {
            unitAnim.SetTrigger(triggerName);
        }

        if(unitSpineAnim!=null)
        {
            TrackEntry track = unitSpineAnim.AnimationState.GetCurrent(0);

            if (track != null && track.Animation.Name != triggerName)
            {
                unitSpineAnim.AnimationState.SetAnimation(0, triggerName, true);
            }
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

            switch (unitAIType)
            {
                case E_INGAME_AI_TYPE.UNIT_INTERACTION:
                    InterActObject();
                    break;
                case E_INGAME_AI_TYPE.UNIT_EVENT:
                    unitAnim?.SetTrigger("Event");
                    break;
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
                if (targetObject != null)
                {
                    targetObject.ExitWork();
                    targetObject = null;
                }
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

        targetObject.DoWork(this, HandleObjectData);
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

        IngameManager.currentManager.SetDroppedObject(dropObject.gameObject);

        dropObject.SetObjectInfo(HandleObjectData.GetFoodId());
        HandleObjectData = null;

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
    public void SetUnitDefaultSpineAnimation()
    {
        Vector3 moveDirect = GetMoveDirect();
        TrackEntry track = unitSpineAnim.AnimationState.GetCurrent(0);
        if (moveDirect == Vector3.zero)
        {
            if (HandleObjectData == null || HandleObjectData.IsEmpty())
            {
                if (track != null && !(track.Animation.Name == "Idle"))
                {
                    unitSpineAnim.AnimationState.SetAnimation(0, "Idle", true);
                }
            }
            else
            {
                if (track != null && !(track.Animation.Name == "Hold"))
                {
                    unitSpineAnim.AnimationState.SetAnimation(0, "Hold", true);
                }
            }
            unitAnim?.SetFloat("Speed", 0);
            IsGround = true;
            ChangeAI(E_INGAME_AI_TYPE.UNIT_IDLE);
            return;
        }

        if (HandleObjectData == null || HandleObjectData.IsEmpty())
        {
            if (track != null && !(track.Animation.Name == "Walk"))
            {
                unitSpineAnim.AnimationState.SetAnimation(0, "Walk", true);
            }
        }
        else
        {
            if (track != null && !(track.Animation.Name == "Lift"))
            {
                unitSpineAnim.AnimationState.SetAnimation(0, "Lift", true);
            }
        }
    }
    public void UnitJump()
    {
        if(!(E_INGAME_AI_TYPE.UNIT_MOVE == unitAIType || E_INGAME_AI_TYPE.UNIT_IDLE == unitAIType))
        {
            return;
        }
        if (characterRigid == null)
        {
            return;
        }

        if (IsGround)
        {
            unitAnim?.SetTrigger("Jump");
            characterRigid.velocity = Vector3.zero;
            characterRigid.angularVelocity = 0.0f;
            characterRigid.AddForce(Vector2.up * characterJumpPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
    protected virtual void UnitMove()
    {
        Vector3 moveDirect = GetMoveDirect();

        SetUnitDefaultSpineAnimation();

        unitAnim?.SetFloat("Speed", moveDirect.magnitude);

        if (moveDirect.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(moveDirect.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        this.transform.Translate(new Vector3(moveDirect.x,0,0) * Time.fixedDeltaTime * characterSpeed);

        //현재 점프중
        Vector3 gravityVector = characterRigid.velocity;

        unitAnim?.SetFloat("YSpeed", gravityVector.y);
    }
    public Vector3 GetMoveDirect()
    {

        //Input받기
        if (joyStick != null)
        {
            Vector3 direct = new Vector3(joyStick.Horizontal, joyStick.Vertical);

#if UNITY_EDITOR
            if (direct == Vector3.zero)
            {
                direct.x = Input.GetAxis("Horizontal");
                direct.y = Input.GetAxis("Vertical");
            }
#endif
            return direct.normalized;
        }

        return Vector3.zero;
    }
    protected virtual void UnitIdle()
    {
        unitAnim?.SetFloat("Speed", 0);
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
        hitresult = Physics2D.Raycast(this.transform.position, Vector3.down, 0.05f, 1 << 9);
        if (hitresult.collider == null)
        {
            IsGround = false;
        }
        else
        {
            ///속도 수정할 방법 필요
            //if(isGround==false)
            //{
            //    characterRigid.velocity = Vector2.zero;
            //}
            IsGround = true;
        }
        Action();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnitJump();
        }
#endif
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
           targetObject.ExitWork();
           targetObject = null;
        }

    }
}
