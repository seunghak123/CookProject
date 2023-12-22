using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObjectViewDataClass : BaseViewDataClass
{
    public int resultFoodId = 0;
}
public class TableObjectView : BaseObjectView
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
    protected virtual bool IsChecked(BaseViewDataClass data)
    {
        if (!IsInit())
        {
            return false;
        }
        if (!(data is TableObjectViewDataClass))
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
