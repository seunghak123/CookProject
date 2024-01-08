using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedObject : BaseToolObject
{
    [SerializeField] private SpriteRenderer droppedSprite;
    private bool isPlate = false;
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = false;
    }
    protected override bool IsCanInterAct(int interActObject)
    {
        return true;
    }
    public void SetObjectInfo(int setInfoId)
    {
        if (setInfoId == 1)
        {
            isPlate = true;
        }
        JFoodObjectData foodObject = JsonDataManager.Instance.GetSingleData<JFoodObjectData>(setInfoId, E_JSON_TYPE.JFoodObjectData);
        droppedSprite.sprite = SpriteManager.Instance.LoadSprite(foodObject.IconFile);
        //Id에 따라 이미지 세팅 setInfoId통해서 아이디 받는다

        //이미지 세팅
    }
    public float GetObjectThrowPower()
    {
        return 100.0f;
    }

    RaycastHit2D hitresult;
    public void FixedUpdate()
    {
        hitresult = Physics2D.Raycast(this.transform.position, Vector3.down, 0.51f, 1 << 9);
        if (hitresult.collider != null)
        {
            //지상 충돌 꺠질것
            //만약 꺠져야하는 애면
            GameResourceManager.Instance.DestroyObject(this.gameObject);
        }

        if(this.transform.position.y<-1000)
        {
            //무한히 떨어지는중
            GameResourceManager.Instance.DestroyObject(this.gameObject);
        }
    }
}
