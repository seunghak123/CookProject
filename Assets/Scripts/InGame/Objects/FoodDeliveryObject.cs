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
            currentWorkRoutine = StartCoroutine(Working());
        }
        else
        {
            //실패했을 때 이벤트 처리 장소 ( 오직 해당 오브젝트 이벤트 )
            currentWorkRoutine = StartCoroutine(WorkingFail());
        }
    }
    private IEnumerator WorkingFail()
    {
        //손에 든거 제거
        yield break;
    }
    public override IEnumerator Working()
    {
        //음식이 라벨에서 움직이고 ~~
        currentWorker.HandleObjectData = null;
        //음식이 제거 되면서 
        //점수 추가

        yield break;
    }
    public override void SetToolData(JToolObjectData newToolData)
    {
        base.SetToolData(newToolData);

        if (deliveryView != null)
        {
            deliveryView.SetBaseSprite(newToolData);
        }
        UpdateUI();
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
