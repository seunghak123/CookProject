using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObjectViewDataClass : BaseViewDataClass
{
    public int resultFoodId = 0;
}
public class TableObjectView : BaseObjectView
{
    [SerializeField] private SpriteRenderer foodSpriteRenderer = null;
    public override void Updated(BaseViewDataClass data)
    {
        if (!IsChecked(data))
        {
            return;
        }
        TableObjectViewDataClass insertData = data as TableObjectViewDataClass;

        if(insertData.resultFoodId!=0)
        {
            JFoodObjectData foodData = JsonDataManager.Instance.GetFoodObject(insertData.resultFoodId);

            foodSpriteRenderer.sprite = SpriteManager.Instance.LoadSprite(foodData.IconFile);
        }
        else
        {
            foodSpriteRenderer.sprite = null;
        }

        //완성이 되었으면 팡 ~ 터지는 이펙트등 추가할 곳
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
