using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectView : MonoBehaviour 
{
    protected abstract bool IsInit();
    public abstract void Updated(BaseViewDataClass updateData);
}
public class BaseViewDataClass
{
}