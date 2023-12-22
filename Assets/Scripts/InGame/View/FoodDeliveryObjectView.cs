using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodDeliveryViewDataClass : BaseViewDataClass
{
    public bool isComplete = false;
}
public class FoodDeliveryObjectView : BaseObjectView
{
    public override void Updated(BaseViewDataClass data)
    {
        if (!IsChecked(data))
        {
            return;
        }
        FoodDeliveryViewDataClass insertData = data as FoodDeliveryViewDataClass;

        //완성이 되었으면 팡 ~ 터지는 이펙트등 추가할 곳
    }
    protected virtual void UpdateCurrentObjectState(int currentIndex)
    {

    }
    protected virtual bool IsChecked(BaseViewDataClass data)
    {
        if (!IsInit())
        {
            return false;
        }
        if (!(data is FoodDeliveryViewDataClass))
        {
            return false;
        }
        return true;
    }

    protected override bool IsInit()
    {
        return true;
    }
}
