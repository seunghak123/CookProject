using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCookerObjectView : ProgressObjectView
{
    [SerializeField] private SpriteRenderer changedSpriteRenderer;

    public override void Updated(BaseViewDataClass data)
    {
        base.Updated(data);

        if (!(data is ProgressedViewDataClass))
        {
            return;
        }
        ProgressedViewDataClass insertData = data as ProgressedViewDataClass;

        //������ ó�� ��� �Ͽ��� �ǳ� 
        //insertData�� currentIndex�� ���� Renderer�� Sprite���� ���
    }
}
