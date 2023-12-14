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

        //insertData의 currentIndex에 따라서 Renderer의 Sprite변경 요망
    }
}
