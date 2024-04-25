using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using System;
using UnityEngine;

namespace CGP
{
    public class ResetActionFlag : StateMachineBehaviour
    {
        CharacterManager character;
        PlayerInputManager playerInputManager;
        CharacterSoundFXManager characterSoundFXManager;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (character == null)
            {
                // animator references the Animator component of the player
                character = animator.GetComponent<CharacterManager>();
                playerInputManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
                characterSoundFXManager = character.GetComponent<CharacterSoundFXManager>();
            }

            if (character.isAlive)
            {
                // if the character is dead don't reset any flags

                if (character.isJumping)
                {
                    characterSoundFXManager.PlayJumpLandSoundFX();
                }

                // THIS IS CALLED WHEN AN ACTION ENDS, AND THE STATE RETURNS TO "EMPTY"
                character.isPerformingAction = false;
                character.canRotate = true;
                character.canMove = true;
                character.applyRootMotion = false;
                character.isJumping = false;
                character.isRolling = false;
                playerInputManager.lightAttackInput = false;
                playerInputManager.heavyAttackInput = false;
                character.isTakingDamage = false;
            }

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
