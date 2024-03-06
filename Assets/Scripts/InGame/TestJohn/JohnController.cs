using Seunghak.Common;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation unitSpineAnim;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ThrowHandlingObject();
            unitSpineAnim.AnimationState.SetAnimation(0, "Throw", false);
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            unitSpineAnim.AnimationState.SetAnimation(0, "Chop", true);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            unitSpineAnim.AnimationState.SetAnimation(0, "Skill_Fire", false);
        }    
    }

    private void ThrowHandlingObject()
    {
        GameObject spawnObject = GameResourceManager.Instance.SpawnObject("DroppedObject");

        if (spawnObject == null)
        {
            Debug.Log("Something Problem");

            return;
        }
        spawnObject.transform.position = this.transform.position + Vector3.up;

        DroppedObject dropObject = spawnObject.GetComponent<DroppedObject>();

        if (dropObject == null)
        {
            return;
        }

        dropObject.SetObjectInfo(16);

        Vector3 throwDirect = new Vector3(this.transform.localScale.x, 1, 0);
        Rigidbody2D dropRigid = dropObject.GetComponent<Rigidbody2D>();
        if (dropRigid != null)
        {
            dropRigid.AddForce(throwDirect * dropObject.GetObjectThrowPower() * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
