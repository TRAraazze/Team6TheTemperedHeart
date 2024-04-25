using CGP;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DevionGames;


public class PlayerHealth : MonoBehaviour
{
    public GameObject hud;
    public GameObject deathScreen;
    public Button reviveButton;

    private Transform initialPlayerPosition;  // Store the initial player position for reset

    IKControl control;

    private HealthSystemForDummies healthSystem;

    public float reviveCooldown = 5.0f;
    public float hitCooldown = 0.5f;
    private bool canTakeDamage = true;

    AudioSource audioSource;

    CGP.PlayerManager player;
     // get the player from the playermanager script which is in the CGP namespace
    
    

    void Start()
    {
        player = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();

        hud = GameObject.Find("HUD");

        deathScreen = hud.FindChild("DeathScreen", true);
        reviveButton = deathScreen.FindChild("Respawn", true).GetComponent<Button>();

        // Get the HealthSystemForDummies component attached to the HUD GameObject
        healthSystem = hud.GetComponentInChildren<HealthSystemForDummies>();

        // Initialize player health, for example, set it to maximum health at the start
        healthSystem.ReviveWithMaximumHealth();

        reviveButton.onClick.AddListener(Revive);

        control = GetComponent<IKControl>();

    }

    void Update()
    {
        // If the cooldown is active, decrement the timer
        if (!canTakeDamage)
        {
            reviveCooldown -= Time.deltaTime;

            // If the cooldown has expired, allow the player to take damage again
            if (reviveCooldown <= 0.0f)
            {
                canTakeDamage = true;
                reviveCooldown = 5.0f; // Reset the cooldown timer for future revivals
            }
        }
    }

    // Function to handle getting hit by an enemy
    public void TakeDamage(float damageAmount)
    {
        // Check if the player is alive before applying damage
        if (healthSystem.IsAlive && canTakeDamage)
        {
            Debug.Log("taking damage currently");
            // Reduce player's health by the damage amount
            healthSystem.AddToCurrentHealth(-damageAmount);

            // Check if the player is still alive after taking damage
            if (!healthSystem.IsAlive && player.isAlive)
            {
                // Perform any additional actions when the player dies, e.g., play death animation, game over logic, etc.
                Debug.Log("Player has died!");
                // play a death animation, show "you died" screen
                player.playerAnimatorManager.PlayTargetActionAnimation("Death_01", true, true);
                player.isAlive = false;

                deathScreen.SetActive(true);
            }
            else
            {
                int damageNum = UnityEngine.Random.Range(0, 2);

                switch (damageNum)
                {
                    case 0:
                        audioSource.PlayOneShot(WorldSoundFXManager.instance.damageSFX1);
                        break;
                    case 1:
                        audioSource.PlayOneShot(WorldSoundFXManager.instance.damageSFX2);
                        break;
                }

                // ONLY PLAY THIS RANDOMLY
                int chance = Random.Range(1,5); // 1 out of 4 chance to stagger

                if (chance != 1)
                {
                    return;
                }

                // NOW stagger player

                // if not dead but still hit, add hit animation (make it a performing action, disable movement and actions for very short while)
                player.playerAnimatorManager.PlayTargetActionAnimation("Get_Hit_01", true, true);
                player.isPerformingAction = true;
                player.canMove = false;
                player.canRotate = false;

                // turn off IKcontrol
                player.isTakingDamage = true;
            }

            canTakeDamage = false;
            StartCoroutine(HitCooldown(hitCooldown));
        }
    }

    // Function to handle player healing (e.g., picking up health pickups)
    public void Heal(float healAmount)
    {
        // Check if the player is alive before applying healing
        if (healthSystem.IsAlive)
        {
            // Increase player's health by the heal amount
            healthSystem.AddToCurrentHealth(healAmount);
        }
    }

    // Function to handle player revival (e.g., respawning after death)
    public void Revive()
    {
        // Revive the player with maximum health
        healthSystem.ReviveWithMaximumHealth();

        player.isAlive = true;
        player.playerAnimatorManager.PlayTargetActionAnimation("Idle_01", true, true);
        player.isPerformingAction = false;
        player.canMove = true;
        player.canRotate = true;

        // Deactivate the death screen upon revival
        deathScreen.SetActive(false);

        reviveCooldown = 5.0f;
    }

    private IEnumerator HitCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canTakeDamage = true;
    }

    // You can add more functions here for additional health-related actions

    // Example of using the script in another class (e.g., an enemy script)
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.CompareTag("Player"))
    //     {
    //         PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //         playerHealth.TakeDamage(enemyDamageAmount);
    //     }
    // }
}
