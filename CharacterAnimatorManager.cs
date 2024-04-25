using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        int vertical;
        int horizontal;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
        {
            float horizontalAmount = horizontalValue;
            float verticalAmount = verticalValue;

            if (isSprinting)
            {
                verticalAmount = 2;
            }

            character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);

            // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTIONS
            // FOR EXAMPLE, IF YOU GET DAMAGED, AND BEGIN PERFORMING A DAMAGE ANIMATION
            // THIS FLAG WILL TURN TRUE IF YOU ARE STUNNED
            // WE CAN THEN CHECK FOR THIS BEFORE ATTEMPTING NEW ACTIONS
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;
        }
    }
}

