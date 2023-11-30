using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCookerObject : BaseObject
{
    //State도 여러 단계 테이블에서 챙겨올것
    private int currentState = 0;

    //해당 내용은 테이블에서 챙겨올것
    private float[] workingTimeArrays = new float[] { 2, 10, 30 };
    protected BasicMaterialData makedMaterial = null;
    private BasicMaterialData preMaterial = null;
    public override void InitObject()
    {
        holdCharacter = false;
        isBlockCharacter = true;
    }
    public override IEnumerator Working()
    {
        //테이블에서 아이디에 따라 취사 단계 설정
        for(int i=0;i< workingTimeArrays.Length;i++)
        {
            yield return WaitTimeManager.WaitForTimeSeconds(workingTimeArrays[i]);

            currentState = i;
        }

        if (WorkEnd())
        {
        }
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        base.DoWork(targetAI, param);
        if(currentState>0)
        {
            //WorkEnd
            WorkEnd();
            //이미 밥솥 일하고있었고, 접근
        }
        else
        {
            if (param != null)
            {
                preMaterial = param;
                StartCoroutine(Working());
            }
        }
    }

    public override bool WorkEnd()
    {
        currentWork = false;

        //푸드 아이디에 따라 makedMaterial 값 세팅
        makedMaterial = new BasicMaterialData();
        //가데이터
        makedMaterial.PushMaterial(3);

        currentWorker.HandleObjectData = makedMaterial;
        //앤 언제나 끝나있음
        return true;
    }
}
