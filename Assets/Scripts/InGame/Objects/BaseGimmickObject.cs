using Seunghak.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseGimmickObject : MonoBehaviour
{
    protected JToolObjectData toolData = null;
    private int objectDataID;
    public int OBJECT_ID
    {
        get { return objectDataID; }
        set
        {
            objectDataID = value;

            if (value < 0)
            {
                objectDataID = 0;
            }
        }
    }


}