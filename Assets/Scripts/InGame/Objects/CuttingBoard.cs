using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seunghak.Common;
public class CuttingBoard : ProgressedBaseObject
{
    private int currentCuttingFood = 0;
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;
    //workingTime은 테이블에서 가져올것
    private float workingTime = 2.0f;
    private BaseAI workingAI = null;

    public override void InitObject()
    {
        holdCharacter = true;
        isBlockCharacter = false;
    }
    public override IEnumerator Working()
    {

        //여기서 progress진행
        //중간에 도마사용을 멈추면 해당 게이지 그대로 가지고 있을것
        yield return WaitTimeManager.WaitForTimeSeconds(workingTime);

        workEnd = true;

        if (IsWorkEnd())
        {
            if(workingAI!=null)
            {
                workingAI.HandleObjectData = makedMaterial;
            }

            workingAI = null;
            preMaterial = null;
            makedMaterial = null;
        }
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI,param);

        workingAI = targetAI;
        preMaterial = param;

        //파라미터값으로 int값을 받아서 id세팅

        //AI 타입에 따라서 묶어 놓고 Trigger 변경
        //targetAI.SetAnimTrigger("")
        //Invoke or Coroutine
        StartCoroutine(Working());
    }

    public override bool IsWorkEnd()
    {
        if (workEnd)
        {
            currentWork = false;

            if (preMaterial == null)
            {
                return false;
            }
            int preFoodId = preMaterial.GetFirstFoodId();

            //푸드 아이디에 따라 makedMaterial 값 세팅
            makedMaterial = new BasicMaterialData();
            //가데이터
            makedMaterial.PushMaterial(2);

            return true;
        }

        return false;
    }

    //상호작용하면 물체를 들어올린다
}
