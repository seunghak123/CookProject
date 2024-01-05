using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatformViewDataClass : BaseViewDataClass
{
    //0~100
    public float disappearPercent = 0.0f;
}
public class DisappearPlatformView : BaseObjectView
{
    public override void Updated(BaseViewDataClass data)
    {
        DisappearPlatformViewDataClass insertData = data as DisappearPlatformViewDataClass;

        Color targetColor = basicSpriteRenderer.color;
        basicSpriteRenderer.color = new Color(targetColor.r, targetColor.g,targetColor.b, 1 - insertData.disappearPercent);
    }
    protected override bool IsInit()
    {
        return true;
    }
}
