using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BaseGimmick : BaseObject
{
    //BaseGimmick 오브젝트 삭제 처리시 현재 Task 지워줘야한다.
    protected CancellationTokenSource disableCancellation = new CancellationTokenSource();
    protected CancellationTokenSource destroyCancellation = new CancellationTokenSource(); 
    private void OnDisable()
    {
        disableCancellation.Cancel();
    }

    private void OnDestroy()
    {
        destroyCancellation.Cancel();
        destroyCancellation.Dispose();
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        //여기서 동작이 필요
    }
    public override void ExitWork()
    {
        //나가졌을 때 처리하는 방법
    }
    public override bool GetIsHold()
    {
        return true;
    }
}
