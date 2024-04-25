using System.Collections;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class PlayerStamina : MonoBehaviour
    {
        // Strings of the player and playerinputmanager objects in the hierarchy
        public string playerObjectName = "UniversalCharacter";
        public string playerInputManagerObjectName = "PlayerInputManager";

        public Slider staminaSlider; // Reference to the Slider component

        public Slider attackCooldownSlider; // Reference to the attack cooldown slider component

        // Buffer is to stop sprint from being used every time there is a little bit of stamina remaining
        float staminaSprintBuffer = 5f;
        float staminaGrowthRate = 15f;
        float staminaDepletedDelay = 2f; // in seconds
        float staminaUsedDelay = 1f; // in seconds

        // Adjust the decay rates as needed
        float sprintStaminaDecay = 10f;
        float rollStaminaDecay = 25f;
        float jumpStaminaDecay = 20f;
        float lightAttackStaminaDecay = 5f;
        float heavyAttackStaminaDecay = 50f;

        // Flags
        bool canStaminaRegen = true;
        bool isRegenDelayOn = false;
        bool isCoroutineRunning = false;

        // TOOD: FIX ATTACK COOLDOWN
        float maxAttackCooldown = 8.4f;
        float curAttackCooldown;
        bool attackCooldownHasReset = false;

        PlayerManager player;
        PlayerInputManager playerInputManager;

        Coroutine lastRoutine = null;

        void Start()
        {
            player = GameObject.Find(playerObjectName).GetComponent<PlayerManager>();
            playerInputManager = GameObject.Find(playerInputManagerObjectName).GetComponent<PlayerInputManager>();

            // set current stamina to the max stamina by default
            player.currentStamina = player.maxStamina;
            UpdateStaminaBar();

            curAttackCooldown = maxAttackCooldown;
            UpdateAttackCooldownBar();
        }

        void Update()
        {
            if (!player.isAlive)
            {
                return;
            }
            // This is to make sure that player can only sprint after a substantial amount of stamina has been regained
            if (player.currentStamina >= staminaSprintBuffer)
            {
                playerInputManager.playerControls.PlayerActions.Sprint.Enable();
            }

            // This is to make sure that player can only roll once they have at least enough to roll again
            if (player.currentStamina >= rollStaminaDecay)
            {
                playerInputManager.playerControls.PlayerActions.Dodge.Enable();
            }

            // Same as above but for jumping
            if (player.currentStamina >= jumpStaminaDecay)
            {
                playerInputManager.playerControls.PlayerActions.Jump.Enable();
            }

            // Same as above but for heavy attacks
            if (player.currentStamina >= heavyAttackStaminaDecay)
            {
                playerInputManager.playerControls.PlayerActions.HeavyAttack.Enable();
            }

            // Same as above but for light attacks
            if (player.currentStamina >= lightAttackStaminaDecay)
            {
                playerInputManager.playerControls.PlayerActions.LightAttack.Enable();
            }

            // Take away stamina a considerable amount for heavy attack
            if (playerInputManager.heavyAttackInput && player.currentStamina > 0)
            {
                DecreaseStamina(heavyAttackStaminaDecay * Time.deltaTime);
            }

            // Make it so that if the current attack cooldown isn't at its max, always grow it back up
            if (curAttackCooldown < maxAttackCooldown)
            {
                IncreaseAttackCooldown(lightAttackStaminaDecay * Time.deltaTime);
            }

            // Take away stamina a little bit for light attack
            if (playerInputManager.lightAttackInput && player.currentStamina > 0)
            {
                if (curAttackCooldown > 0)
                {
                    DecreaseStamina(lightAttackStaminaDecay * Time.deltaTime);
                }

                // we want the cooldown to be used up the first time lightAttackInput is true so that it sets it to 0
                // then for the rest of the input we want the cooldown to slowly regain at the same rate that stamina decreases
                if (!attackCooldownHasReset && curAttackCooldown == maxAttackCooldown)
                {
                    ResetAttackCooldown();
                }
            }

            // if attack cooldown is full then set attackCooldownHasReset to false
            if (curAttackCooldown == maxAttackCooldown)
            {
                attackCooldownHasReset = false;
            }

            // Decrease stamina based on sprinting
            if (player.isSprinting && player.currentStamina > 0)
            {
                DecreaseStamina(sprintStaminaDecay * Time.deltaTime);
            }

            // Decrease stamina by a certain amount when rolling
            else if (player.isRolling && player.currentStamina > 0)
            {
                DecreaseStamina(rollStaminaDecay * Time.deltaTime);
            }

            if (player.isJumping && player.currentStamina > 0)
            {
                DecreaseStamina(jumpStaminaDecay * Time.deltaTime);
            }

            // only regen stamina when not performing an action that decays it
            else
            {
                if (!canStaminaRegen)
                {
                    if (!isRegenDelayOn && player.currentStamina == 0)
                    {
                        lastRoutine = StartCoroutine(RegenDelay(staminaDepletedDelay));
                    }
                    else if (!isRegenDelayOn && player.currentStamina > 0)
                    {
                        lastRoutine = StartCoroutine(RegenDelay(staminaUsedDelay));
                    }
                    else if (isRegenDelayOn && player.currentStamina > 0 && isCoroutineRunning)
                    {
                        isCoroutineRunning = false;
                        StopCoroutine(lastRoutine);
                        lastRoutine = StartCoroutine(RegenDelay(staminaUsedDelay));
                    }
                }
                else
                {
                    IncreaseStamina(staminaGrowthRate * Time.deltaTime);
                }
            }
        }

        IEnumerator RegenDelay(float amount)
        {
            // isRegenDelayOn is a flag that checks whether or not this coroutine is already running
            isRegenDelayOn = true;
            yield return new WaitForSeconds(amount);
            canStaminaRegen = true;
            isRegenDelayOn = false;
        }

        void DecreaseStamina(float amount)
        {
            player.currentStamina -= amount;
            player.currentStamina = Mathf.Clamp(player.currentStamina, 0f, player.maxStamina);

            UpdateStaminaBar();

            // adds a short delay before stamina can regen
            canStaminaRegen = false;
            isCoroutineRunning = true;

            // Stop sprinting and rolling if stamina reaches 0 from an action
            if (player.currentStamina == 0)
            {
                player.isSprinting = false;
                player.isRolling = false;
                playerInputManager.playerControls.PlayerActions.Sprint.Disable();
            }

            // disable actions by default and only enable in update function
            playerInputManager.playerControls.PlayerActions.Dodge.Disable();
            playerInputManager.playerControls.PlayerActions.Jump.Disable();
            playerInputManager.playerControls.PlayerActions.HeavyAttack.Disable();
            playerInputManager.playerControls.PlayerActions.LightAttack.Disable();
        }

        void IncreaseStamina(float amount)
        {
            player.currentStamina += amount;
            player.currentStamina = Mathf.Clamp(player.currentStamina, 0f, player.maxStamina);

            UpdateStaminaBar();
        }

        void UpdateStaminaBar()
        {
            float fillAmount = player.currentStamina / player.maxStamina;
            staminaSlider.value = fillAmount; // Use the value property of the Slider
        }

        void ResetAttackCooldown()
        {
            curAttackCooldown = 0;
            attackCooldownHasReset = true;

            UpdateAttackCooldownBar();
        }

        void IncreaseAttackCooldown(float amount)
        {
            curAttackCooldown += amount;
            curAttackCooldown = Mathf.Clamp(curAttackCooldown, 0f, maxAttackCooldown);

            UpdateAttackCooldownBar();
        }

        void UpdateAttackCooldownBar()
        {
            float fillAmount = curAttackCooldown / maxAttackCooldown;
            attackCooldownSlider.value = fillAmount;
        }
    }

}
