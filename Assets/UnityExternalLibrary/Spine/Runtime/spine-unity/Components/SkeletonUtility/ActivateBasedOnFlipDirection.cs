/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated September 24, 2021. Replaces all prior versions.
 *
 * Copyright (c) 2013-2021, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;

namespace Spine.Unity {

	/// <summary>
	/// Utility component to support flipping of 2D hinge chains (chains of HingeJoint2D objects) along
	/// with the parent skeleton by activating the respective mirrored versions of the hinge chain.
	/// Note: This component is automatically attached when calling "Create Hinge Chain 2D" at <see cref="SkeletonUtilityBone"/>,
	/// do not attempt to use this component for other purposes.
	/// </summary>
	public class ActivateBasedOnAnim_FlipDirection : MonoBehaviour {

		public SkeletonRenderer skeletonRenderer;
		public SkeletonGraphic skeletonGraphic;
		public GameObject activeOnNormalX;
		public GameObject activeOnAnim_FlippedX;
		HingeJoint2D[] jointsNormalX;
		HingeJoint2D[] jointsAnim_FlippedX;
		ISkeletonComponent skeletonComponent;

		bool wasAnim_FlippedXBefore = false;

		private void Start () {
			jointsNormalX = activeOnNormalX.GetComponentsInChildren<HingeJoint2D>();
			jointsAnim_FlippedX = activeOnAnim_FlippedX.GetComponentsInChildren<HingeJoint2D>();
			skeletonComponent = skeletonRenderer != null ? (ISkeletonComponent)skeletonRenderer : (ISkeletonComponent)skeletonGraphic;
		}

		private void FixedUpdate () {
			bool isAnim_FlippedX = (skeletonComponent.Skeleton.ScaleX < 0);
			if (isAnim_FlippedX != wasAnim_FlippedXBefore) {
				HandleAnim_Flip(isAnim_FlippedX);
			}
			wasAnim_FlippedXBefore = isAnim_FlippedX;
		}

		void HandleAnim_Flip (bool isAnim_FlippedX) {
			GameObject gameObjectToActivate = isAnim_FlippedX ? activeOnAnim_FlippedX : activeOnNormalX;
			GameObject gameObjectToDeactivate = isAnim_FlippedX ? activeOnNormalX : activeOnAnim_FlippedX;

			gameObjectToActivate.SetActive(true);
			gameObjectToDeactivate.SetActive(false);

			ResetJointPositions(isAnim_FlippedX ? jointsAnim_FlippedX : jointsNormalX);
			ResetJointPositions(isAnim_FlippedX ? jointsNormalX : jointsAnim_FlippedX);
			CompensateMovementAfterAnim_FlipX(gameObjectToActivate.transform, gameObjectToDeactivate.transform);
		}

		void ResetJointPositions (HingeJoint2D[] joints) {
			for (int i = 0; i < joints.Length; ++i) {
				HingeJoint2D joint = joints[i];
				Transform parent = joint.connectedBody.transform;
				joint.transform.position = parent.TransformPoint(joint.connectedAnchor);
			}
		}

		void CompensateMovementAfterAnim_FlipX (Transform toActivate, Transform toDeactivate) {
			Transform targetLocation = toDeactivate.GetChild(0);
			Transform currentLocation = toActivate.GetChild(0);
			toActivate.position += targetLocation.position - currentLocation.position;
		}
	}
}
