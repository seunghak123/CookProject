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

        //�ϼ��� �Ǿ����� �� ~ ������ ����Ʈ�� �߰��� ��
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
