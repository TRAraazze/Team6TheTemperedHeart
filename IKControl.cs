using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class IKControl : MonoBehaviour
    {
        protected Animator animator;
        [SerializeField] PlayerManager player;

        public bool ikActive = true;
        public GameObject leftHandObj;

        private void Start()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            // if player is dead disable IK
            if (!player.isAlive || player.isTakingDamage)
            {
                ikActive = false;
            }
            else
            {
                ikActive = true;
            }
        }

        private void OnAnimatorIK()
        {
            if (animator)
            {
                // if the IK is active, set the position and rotation directly to the goal
                if (ikActive)
                {


                    // set the left hand target position and rotation, if one has been assigned
                    if (leftHandObj.transform != null)
                    {
                        if (player.isSprinting)
                        {
                            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.transform.position);
                            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.transform.rotation);
                        }
                        else
                        {
                            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.transform.position);
                            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.transform.rotation);
                        }

                    }
                }

                // if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                }
            }
        }
    }
}
