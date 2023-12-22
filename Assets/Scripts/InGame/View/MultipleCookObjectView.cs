using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleCookObjectView : ProgressObjectView
{
    [SerializeField] private Image[] foodInsertLists;

    public override void Updated(BaseViewDataClass data)
    {
        base.Updated(data);

        if (!(data is ProgressedViewDataClass))
        {
            return;
        }
        ProgressedViewDataClass insertData = data as ProgressedViewDataClass;

        UpdateInsertFoodList(insertData.foodInsertLists);

        //������ ó�� ��� �Ͽ��� �ǳ� 
        //insertData�� currentIndex�� ���� Renderer�� Sprite���� ���
    }
    private void UpdateInsertFoodList(List<int> insertFoodResult)
    {
        for(int i=0; i<insertFoodResult.Count; i++)
        {
            if(foodInsertLists.Length>i)
            {
                JFoodObjectData foodObject = JsonDataManager.Instance.GetFoodObject(insertFoodResult[i]);

                Sprite foodImage = SpriteManager.Instance.LoadSprite(foodObject.IconFile);
                foodInsertLists[i].sprite = foodImage;
            }
        }
    }
}
