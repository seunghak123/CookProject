using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodResut : BaseObject
{
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = true;
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        
        //FoodResult를 요리 투입매니저로 전달 //param.GetFoodResult()
        //
        //요리 투입
    }

    public override bool WorkEnd()
    {

        return true;
    }
}
