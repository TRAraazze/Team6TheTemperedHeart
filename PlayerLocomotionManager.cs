using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

namespace CGP
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float runningSpeed = 4.5f;
        [SerializeField] float sprintingSpeed = 6.5f;
        [SerializeField] float rotationSpeed = 15;

        [Header("Jump")]
        [SerializeField] float jumpHeight = 2.5f;
        [SerializeField] float jumpForwardSpeed = 4;
        [SerializeField] float freeFallSpeed = 2;
        private Vector3 jumpDirection;

        [Header("Dodge")]
        private Vector3 rollDirection; // not dodge direction, only for roll (not for backstep)

        public IKControl control;
        public Animator animator;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
            control = GetComponent<IKControl>();
            animator = GetComponent<Animator>();
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
            HandleJumpingMovement();
            HandleFreeFallMovement();
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
        }

        private void HandleGroundedMovement()
        {
            if (!player.canMove)
                return;

            GetMovementValues();

            // OUR MOVE DIRECTION IS BASED ON OUR CAMERA'S PERSPECTIVE AND OUR INPUTS
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0; // don't want to move up only left and right or up and down

            if (player.isSprinting)
            {
                player.isMoving = true;
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.instance.moveAmount == 1)
                {
                    player.isMoving = true;
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.instance.moveAmount == 0)
                {
                    player.isMoving = false;
                }
            }
        }

        private void HandleJumpingMovement()
        {
            if (player.isJumping)
            {
                player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
            }
        }

        private void HandleFreeFallMovement()
        {
            if (!player.isGrounded)
            {
                Vector3 freeFallDirection;

                freeFallDirection = PlayerCamera.instance.transform.forward * PlayerInputManager.instance.verticalInput;
                freeFallDirection += PlayerCamera.instance.transform.right * PlayerInputManager.instance.horizontalInput;
                freeFallDirection.y = 0;

                player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            Vector3 targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                player.isSprinting = false;
            }

            if (PlayerInputManager.instance.moveAmount == 1)
            {
                player.isSprinting = true;
            }
            else
            {
                player.isSprinting = false;
            }

        }

        // this is labelled "attempt" because it may depend on if the player has stamina or not
        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return;

            //if (!player.canUseRoll)
            //    return;

            // IF WE ARE MOVING WHEN WE ATTEMPT TO DODGE, WE PERFORM A ROLL
            if (PlayerInputManager.instance.moveAmount == 1)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
                rollDirection.y = 0; // not up or down, only left or right
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);

                player.isRolling = true;
            }
            // IF WE ARE STATIONARY, WE PERFORM A BACKSTEP BUT ONLY IF WE ARE GROUNDED - REMOVED !!!
            else
            {
                control.ikActive = false;
                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
                StartCoroutine(ToggleIKActive());
                player.isRolling = true; // technically we are not rolling but this should still give i-frames
            }
        }

        IEnumerator ToggleIKActive()
        {
            yield return new WaitForSeconds(3); // might need to change this arbitrary value
            control.ikActive = true;
        }

        public void AttemptToPerformJump()
        {
            if (player.isPerformingAction)
                return;

            if (player.isJumping)
                return;

            if (!player.isGrounded)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_Start_01", false, true, true);

            player.isJumping = true;

            jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            jumpDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            jumpDirection.y = 0;

            if (jumpDirection != Vector3.zero)
            {
                // IF WE ARE SPRINTING, JUMP DIRECTION IS AT FULL DISTANCE
                if (player.isSprinting)
                {
                    jumpDirection *= 1;
                }
                // IF WE ARE RUNNING, JUMP DIRECTION IS AT HALF DISTANCE
                else if (PlayerInputManager.instance.moveAmount == 1)
                {
                    jumpDirection *= 0.5f;
                }
            }
        }

        public void ApplyJumpingVelocity()
        {
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }
    }

}
