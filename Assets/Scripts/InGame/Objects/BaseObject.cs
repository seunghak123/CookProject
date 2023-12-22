using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseObject : MonoBehaviour
{
    [SerializeField] protected bool holdCharacter = false;
    [SerializeField] protected bool isBlockCharacter = false;
    protected bool workEnd = false;
    protected bool currentWork = false;
    protected BaseAI currentWorker = null;
    protected JToolObjectData toolData = null;
#if UNITY_EDITOR
    [SerializeField]
    private int objectDataID;
#endif
    public int OBJECT_ID
    {
        get { return objectDataID; }
        set {
            objectDataID = value;

            if (value < 0)
            {
                objectDataID = 0;
            }
        }
    }
    protected virtual bool IsCanInterAct(int interActObject)
    {
        //Type1번 완료
        List<JRecipeData> recipeData = JsonDataManager.Instance.GetOutputRecipeDatas(OBJECT_ID);

        if (recipeData!=null && recipeData.ConvertAll<int>(find => find.FoodName).Contains(interActObject))
        {
            return true;
        }

        return false;
    }
    protected virtual void Awake()
    {
        // 오브젝트 초기화
        InitObject();
    }
    protected virtual void UpdateUI()
    {
    }
    public virtual void InitObject() 
    {
        //이전에 sprite 세팅

        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();

        if (spriteRender!=null)
        {
            Sprite resourceSprite = GameResourceManager.Instance.LoadObject("이미지 명") as Sprite;

            if(resourceSprite==null)
            {
                return;
            }

            spriteRender.sprite = resourceSprite;

            Vector2 spriteSize = resourceSprite.bounds.size;

            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if(boxCollider!=null)
            {
                boxCollider.size = spriteSize;
                boxCollider.offset = new Vector2((spriteSize.x / 2), 0);
            }
        }
        //기존에는 파일 읽고, Id 세팅한다
    }
    public bool GetIsHold()
    {
        return holdCharacter && IsWork();
    }
    public virtual void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        currentWork = true;
        currentWorker = targetAI; 

        //AI 타입에 따라서 묶어 놓고 Trigger 변경
        //targetAI.SetAnimTrigger("")
        //Invoke or Coroutine
        //상태에 따라서 달리진행
    }
    public virtual void ExitWork()
    {
        //나가졌을 때 처리하는 방법
    }
    public virtual IEnumerator Working()
    {
        yield break;
    }
    public bool IsWork()
    {
        return currentWork;
    }
    public virtual bool IsWorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;

            return true;
        }

        return false;
    }
    public void SetToolData(JToolObjectData newToolData)
    {
        toolData = newToolData;
    }
}