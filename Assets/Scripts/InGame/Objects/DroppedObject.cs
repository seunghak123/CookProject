using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedObject : BaseObject
{
    [SerializeField] private SpriteRenderer droppedSprite;
    private bool isPlate = false;
    private float lifeTime = 10.0f;
    private float currentLifeTime = 0.0f;
    private int currentFoodId = 0;

    public override bool GetIsHold()
    {
        return false;
    }
    public override void InitObject()
    {
        currentLifeTime = 0;
    }
    public void SetObjectInfo(int setInfoId)
    {
        if (setInfoId == 1)
        {
            isPlate = true;
        }
        currentFoodId = setInfoId;
        JFoodObjectData foodObject = JsonDataManager.Instance.GetSingleData<JFoodObjectData>(setInfoId, E_JSON_TYPE.JFoodObjectData);
        droppedSprite.sprite = SpriteManager.Instance.LoadSprite(foodObject.IconFile);
        //Id에 따라 이미지 세팅 setInfoId통해서 아이디 받는다

        //이미지 세팅
    }
    public float GetObjectThrowPower()
    {
        return 100.0f;
    }
    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        if(currentLifeTime>lifeTime)
        {
            IngameManager.currentManager.RemoveDroppedObject(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(currentFoodId!=0)
            {
                
            }
            //other.GetComponent<BaseAI>()
            //플레이어 부딪치면 바로 들어지게 처리
            //other.gameObject.
        }
    }
}
