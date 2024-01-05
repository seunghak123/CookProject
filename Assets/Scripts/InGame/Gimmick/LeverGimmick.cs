using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGimmick : BaseGimmick
{
    [SerializeField] private List<BaseGimmick> interActGimmick = new List<BaseGimmick>();
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        if (targetAI != null)
        {
            InterAct(targetAI, param);
        }
        //여기서 동작이 필요

    }

    public override void InitObject()
    {
    }
    public override void ExitWork()
    {
        for (int i = 0; i < interActGimmick.Count; i++)
        {
            interActGimmick[i].ExitWork();
        }
    }
    public override bool GetIsHold()
    {
        return false;
    }
    public async UniTask InterAct(BaseAI movedAI, BasicMaterialData param)
    {
        await UniTask.NextFrame();
        //지정된 오브젝트 활동하도록 할것
        for (int i=0;i<interActGimmick.Count;i++)
        {
            interActGimmick[i].DoWork(movedAI, param);
        }
        await UniTask.NextFrame();
    }
}
