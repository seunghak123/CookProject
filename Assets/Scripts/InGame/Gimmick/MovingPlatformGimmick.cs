using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MovingPlatformGimmick : BaseGimmick
{
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
    public override async UniTask InterAct()
    {
        //disable시 WebRequest 취소 예시
        //await UnityWebRequest.Get("http://google.co.jp").SendWebRequest().WithCancellation(disableCancellation.Token);
    }
}
