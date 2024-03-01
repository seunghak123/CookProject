using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatform : BaseGimmick
{
    [SerializeField] private DisappearPlatformView platformView;
    [SerializeField] private DisappearPlatformViewDataClass disappearViewData;
    [SerializeField] private Collider2D platformCol;

    [SerializeField] private float disappearTime = 2.0f;
    [SerializeField] private float respawnTime = 4.0f;

    private bool isWork = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player" && !isWork)
        {
            isWork = true;
            InterAct();
        }
    }
    private void OnDisable()
    {
        
    }
    public override void InitObject()
    {
        if (disappearViewData == null)
        {
            disappearViewData = new DisappearPlatformViewDataClass();
        }
        if (platformView != null)
        {
            platformView.Updated(disappearViewData);
        }
    }
    public override void DoWork(BaseAI targetAI, BasicMaterialData param)
    {
        return;
    }
    public async UniTask InterAct()
    {
        DisappearPlatformViewDataClass interActData = new DisappearPlatformViewDataClass();
        float delayTime = 0.0f;
        while (true)
        {
            if(disappearTime<delayTime)
            {
                platformCol.enabled = false;
                break;
            }

            await UniTask.NextFrame();
            if (!IngameManager.currentManager.isPause)
            {
                delayTime += Time.fixedDeltaTime;
            }

            interActData.disappearPercent = delayTime / disappearTime;

            platformView.Updated(interActData);
        }

        delayTime = 0.0f;
        while (true)
        {
            if (respawnTime < delayTime)
            {
                interActData.disappearPercent = 0.0f;
                platformView.Updated(interActData);
                isWork = false;
                platformCol.enabled = true;
                break;
            }
            await UniTask.NextFrame();
            delayTime += Time.fixedDeltaTime;
        }
    }
}
