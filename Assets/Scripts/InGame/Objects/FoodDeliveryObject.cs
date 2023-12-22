using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeliveryObject : BaseObject
{
    [SerializeField] protected FoodDeliveryObjectView deliveryView;
    protected FoodDeliveryViewDataClass foodDeliveryViewData;
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = true;

        deliveryView = GetComponent<FoodDeliveryObjectView>();
    }

    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);

        //param.GetFoodId() ==  음식이 없으면 점수 0 점

        bool isSuccess = IngameManager.currentManager.CheckRecipeComplete(param);

        if(isSuccess)
        {
            //팡파레 울리는 등 성공시 이벤트 넣어줄곳
            StartCoroutine(Working());
        }
        else
        {
            //실패했을 때 이벤트 처리 장소 ( 오직 해당 오브젝트 이벤트 )
            StartCoroutine(WorkingFail());
        }
    }
    private IEnumerator WorkingFail()
    {
        yield break;
    }
    public override IEnumerator Working()
    {

        yield break;
    }
    protected override void UpdateUI()
    {
        if (foodDeliveryViewData == null)
        {
            foodDeliveryViewData = new FoodDeliveryViewDataClass();
        }

        if (deliveryView != null)
        {
            deliveryView.Updated(foodDeliveryViewData);
        }
    }

    public override bool IsWorkEnd()
    {
        return true;
    }
}
