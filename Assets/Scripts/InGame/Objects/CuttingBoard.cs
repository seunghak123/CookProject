using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : BaseObject
{
    public override void InitObject()
    {
        holdCharacter = true;
        isBlockCharacter = false;
    }
    //상호작용하면 물체를 들어올린다
}
