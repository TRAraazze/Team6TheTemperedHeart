using Unity.VisualScripting;
using UnityEngine;


namespace CGP
{

    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        
        public PlayerManager player;

        public PlayerControls playerControls;

        public ParticleSystem heavyAttackParticleEffectFire1 = null;
        public ParticleSystem heavyAttackParticleEffectFire2 = null;
        public ParticleSystem heavyAttackParticleEffectFire3 = null;
        public ParticleSystem heavyAttackParticleEffectIce1 = null;
        public ParticleSystem heavyAttackParticleEffectIce2 = null;
        public ParticleSystem heavyAttackParticleEffectIce3 = null;
        public ParticleSystem heavyAttackParticleEffectAir1 =  null;
        public ParticleSystem heavyAttackParticleEffectAir2 = null;
        public ParticleSystem heavyAttackParticleEffectAir3 = null;

        public GameObject dome;
        public GameObject blastDome;
        public GameObject slash; 

        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool sprintInput = false;
        [SerializeField] bool jumpInput = false;
        [SerializeField] public bool lightAttackInput = false;
        [SerializeField] public bool heavyAttackInput = false;
        [SerializeField] bool healInput = false;

        public bool isMouseFree = false;

        public enum GameState
        {
            None,
            Ice1,
            Ice2,
            Ice3,
            Fire1,
            Fire2,
            Fire3,
            Air1,
            Air2,
            Air3
        }

        public GameState currentGameState = GameState.None; // Set the default game state

        // Method to set the game state
        public void SetGameState(GameState newGameState)
        {
            currentGameState = newGameState;
            Debug.Log("Game state changed to: " + currentGameState.ToString());
        }

        

        private void Awake()
            {
            
                if (instance == null)
                {
                    instance = this;

                    instance.enabled = true;
                }
                else
                {
                    //Destroy(gameObject);
                }
            
            }

        // fires when game object is active in the scene
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

                // HOLDING THE INPUT, SETS THE BOOL TO TRUE
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                // RELEASING THE INPUT, SETS THE BOOL TO FALSE
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

                playerControls.PlayerActions.HeavyAttack.performed += i => heavyAttackInput = true;

                playerControls.PlayerActions.LightAttack.performed += i => lightAttackInput = true;

                // toggles the mouse to be free or not
                playerControls.OtherActions.ToggleMouse.performed += i => isMouseFree = !isMouseFree;

                playerControls.PlayerActions.Heal.performed += i => healInput = true;
            }

            playerControls.Enable();
        }

        // IF WE MINIMIZE OR LOWER THE WINDOW, STOP ADJUSTING INPUTS
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            if (!isMouseFree)
            {
                if (!player.isPerformingAction)
                    player.canMove = true;

                HandleAllInputs();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                player.canMove = false;
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, 0, false);
                playerControls.PlayerActions.Disable();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void HandleAllInputs()
        {
            HandleCameraMovementInput();
            HandlePlayerMovementInput();
            HandleDodgeInput();
            HandleSprinting();
            HandleJumpInput();
            HandleAttacking();
            HandleHealing();
        }

        private void HandleHealing()
        {
            // if player is trying to heal and they have at least 1 health potion in their inventory
            if (healInput)
            {
                //Debug.Log("health input");
                // remove one health potion from the inventory
                PotionCounter.DecreaseHealthPotions();
                healInput = false;
            }
        }

        // MOVEMENT

        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // RETURNS THE ABSOLUTE NUMBER, (Meaning number without the negative sign, so its always positive)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // CLAMP VALUE SO AS LONG AS WE ARE MOVING, MOVE AMOUNT IS 1
            if (moveAmount > 0)
            {
                moveAmount = 1;
            }

            // WHY DO WE PASS 0 ON THE HORIZONTAL? BECAUSE WE ONLY WANT NON-STRAFING MOVEMENT
            // WE USE THE HORIZONTAL WHEN WE ARE STRAFING OR LOCKED ON

            if (player == null)
                return;

            // IF WE ARE NOT LOCKED ON, ONLY USE THE MOVE AMOUNT
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

            // IF WE ARE LOCKED ON PASS THE HORIZONTAL MOVEMENT AS WELL
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        // ACTIONS

        private void HandleDodgeInput()
        {
            //player.rollCooldownTimer.Tick();

            //if (dodgeInput && player.canUseRoll)
            if (dodgeInput)
            {
                dodgeInput = false;
                //player.canUseRoll = false;

                // if we want player to move around when menu is open, do not perform a dodge when the UI is activated (RETURN NOTHING)

                player.playerLocomotionManager.AttemptToPerformDodge();

                //player.rollCooldownTimer.StartTimer();
            }
        }

        private void HandleSprinting()
        {
            if (sprintInput)
            {
                player.playerLocomotionManager.HandleSprinting();
            }
            else
            {
                player.isSprinting = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jumpInput)
            {
                jumpInput = false;

                // IF WE HAVE A UI WINDOW OPEN, SIMPLY RETURN WITHOUT DOING ANYTHING

                // ATTEMPT TO PERFORM JUMP
                player.playerLocomotionManager.AttemptToPerformJump();
            }
        }

        private void HandleAttacking()
        {
            player.attackCounterResetTimer.Tick();

            if (player.isPerformingAction || player.isJumping)
            {
                player.isAttacking = false;
                
                return; 
            }

            // if player is already attacking don't let any other attack input do anything
            if (player.isAttacking)
            {
                player.isMoving = false;
                return;
            }
            

            // Make player.isAttacking true if there is any sort of attack input, but only make one input true at a time
            if (lightAttackInput)
            {
                player.isAttacking = true;
                heavyAttackInput = false;
            }
            else if (heavyAttackInput)
            {
                player.isAttacking = true;
                lightAttackInput = false; 
            }

            if (player.isAttacking && player.isGrounded)
            {
                // IF ATTACK IS A HEAVY ATTACK
                if (heavyAttackInput)
                {



                    player.playerAnimatorManager.PlayTargetActionAnimation("Heavy_Attack_01", true, true);
                    Invoke(nameof(ActivateHeavyAttackParticleEffect), 1); // Adjust delayTime as needed
                    player.isPerformingAction = true;
                }
                else if (lightAttackInput)
                {
                    // LIGHT ATTACKS
                    // IF WE HIT ATTACK COUNTER OF NUMBER OF ATTACKS - 1 OR MORE, RESET BACK TO 0
                    if (player.currentAttackCounter > player.numberAttacks - 1)
                    {
                        player.currentAttackCounter = 0;
                    }

                    if (player.currentAttackCounter == 0)
                    {
                        player.playerAnimatorManager.PlayTargetActionAnimation("Attack_01", true, true);
                        player.isPerformingAction = true;
                    }
                    else if (player.currentAttackCounter == 1)
                    {
                        player.playerAnimatorManager.PlayTargetActionAnimation("Attack_02", true, true);
                        player.isPerformingAction = true;
                    }
                    else if (player.currentAttackCounter == 2)
                    {
                        player.playerAnimatorManager.PlayTargetActionAnimation("Attack_03", true, true);
                        player.isPerformingAction = true;
                    }

                    player.isAttacking = false;

                    // INCREMENT ATTACK COUNTER
                    player.currentAttackCounter++;
                    player.attackCounterResetTimer.StartTimer();
                }
            }
            else if (player.isAttacking && !player.isGrounded)
            {
                player.isAttacking = false;
            }
        }

        private void ActivateHeavyAttackParticleEffect()
        {
            // Check if the particle effect GameObject is assigned
 
                switch (currentGameState)
                {
                    case GameState.Ice1:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectIce1);
                        slash.SetActive(true);
                    break;
                    case GameState.Ice2:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectIce2);
                        blastDome.SetActive(true);
                        break;
                    case GameState.Ice3:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectIce3);
                        dome.SetActive(true);
                    break;
                    case GameState.Fire1:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectFire1);
                        slash.SetActive(true);
                        break;
                    case GameState.Fire2:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectFire2);
                        blastDome.SetActive(true);
                        break;
                    case GameState.Fire3:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectFire3);
                        dome.SetActive(true);
                    break;
                    case GameState.Air1:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectAir1);
                        slash.SetActive(true);
                        break;
                    case GameState.Air2:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectAir2);
                        blastDome.SetActive(true);  
                        break;
                    case GameState.Air3:
                        PlayHeavyAttackParticleEffect(heavyAttackParticleEffectAir3);
                        dome.SetActive(true);
                    break;
                    default:
                        break;
                }
        }

        private void PlayHeavyAttackParticleEffect(ParticleSystem particleEffect)
        {
            if (particleEffect != null)
            {
                particleEffect.Play();
                Invoke(nameof(StopParticleEffect), particleEffect.main.duration);
            }
            else
            {
                Debug.LogWarning("Particle Effect GameObject is not assigned.");
            }
        }

        private void StopParticleEffect()
        {
            // Stop the particle effect based on the current game state
            switch (currentGameState)
            {
                case GameState.Ice1:
                    heavyAttackParticleEffectIce1?.Stop();
                    slash.SetActive(false);
                    break;
                case GameState.Ice2:
                    heavyAttackParticleEffectIce2?.Stop();
                    blastDome.SetActive(false);
                    break;
                case GameState.Ice3:
                    heavyAttackParticleEffectIce3?.Stop();
                    dome.SetActive(false);
                    break;
                case GameState.Fire1:
                    heavyAttackParticleEffectFire1?.Stop();
                    slash.SetActive(false);
                    break;
                case GameState.Fire2:
                    heavyAttackParticleEffectFire2?.Stop();
                    blastDome.SetActive(false);
                    break;
                case GameState.Fire3:
                    heavyAttackParticleEffectFire3?.Stop();
                    dome.SetActive(false);
                    break;
                case GameState.Air1:
                    heavyAttackParticleEffectAir1?.Stop();
                    slash.SetActive(false);
                    break;
                case GameState.Air2:
                    heavyAttackParticleEffectAir2?.Stop();
                    blastDome.SetActive(false);
                    break;
                case GameState.Air3:
                    heavyAttackParticleEffectAir3?.Stop();
                    dome.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Invalid game state");
                    break;
            }
        }


    }


}

