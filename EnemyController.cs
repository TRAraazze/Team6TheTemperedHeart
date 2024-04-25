using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CGP;
using System.Text;


public class EnemyController : MonoBehaviour
{
    // Dictates how far the enemy can detect players
    public float lookRadius = 8f;

    // Names for animations
    public string walkAnimationName = "walk";
    public string idleAnimationName = "idle";
    public string[] attackAnimationNames = { "attack1", "attack2" };
    public int numberOfAttackAnimations = 1;
    public string damageAnimationName = "takeDamage";
    public string deathAnimationName = "death";

    // Walk speed changer info
    string walkSpeedName = "walkSpeed";
    public float animationSpeedDivider = 2.5f;

    [Header("Enemy Stats")]
    public int damageDealt = 100; // how much damage enemy can do to the player
    public int health = 500;
    private float maxHealth;
    public int staggerChance = 1; // measured if (Random.Range(1, staggerChance+1) == staggerChance)
    public float randomSpeedMin = 1.5f;
    public float randomSpeedMax = 4f;
    public float randomWaitSpeedMin = 2;
    public float randomWaitSpeedMax = 4;

    // Flags
    private bool canBeStaggered = false;
    public bool isDead = false;
    public bool canDealDamage = false;
    public bool isAttacking = false;
    public bool canBeDamaged = true;
    public bool isMoving = false;
    public bool speedChanged = false;
    public bool isTakingDamage = false;
    private bool isWaiting = false;
    private bool isAttackWaiting = false;

    // Other
    private NavMeshAgent agent;
    public Rigidbody enemyBody;
    private Vector3 curDirection;
    public Animator animator;
    public Timer damageCooldownTimer;
    public float damageCooldownAmount = 1.5f;
    public bool isDamageCooldownOver = true;
    HealthSystemForDummies healthSystem;
    HealthBar healthBar;

    int currentAttackCounter = 1;
    Coroutine last;
    AudioSource audioSource;

    public int isNegative = -1; // bandaid fix for spider specifically

    [Header("Target Stats")]
    private Transform target;
    PlayerHealth targetHealth;
    public bool canTargetTakeDamage = false;
    public PlayerManager playerManager;
    public GameObject playerTarget;

    // wander script
    public float wanderRadius = 10;
    public float wanderTimer;
    public float wanderTimeMin = 3f;
    public float wanderTimeMax = 5f;
    public bool doesWander = true;
    private float timer;

    public int lootIndex;
    public int currencyDrop;
    public int lootAmount;
    public int questIndex;
    public int questAmount;

    // Runs first
    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        // set the target to the player
        target = playerTarget.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyBody = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystemForDummies>();
        healthBar = GetComponentInChildren<HealthBar>();
        audioSource = playerTarget.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        maxHealth = health;

        damageCooldownTimer = new Timer(damageCooldownAmount);
    }

    // Runs when enabled
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(wanderTimeMin, wanderTimeMax);
        timer = wanderTimer;

        damageCooldownTimer.OnTimerDone += ResetDamageCooldown;
    }

    // When the enemy damage cooldown resets, the enemy can be damaged again and is not currently taking damage
    private void ResetDamageCooldown()
    {
        canBeDamaged = true;
        isTakingDamage = false;
        agent.updatePosition = true;
    }

    // Creates a random spot for the NavAgent to navigate to
    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    // Deals with random wander script and facing and following player
    void Update()
    {
        // ticks the damage cooldown timer in case it's running
        damageCooldownTimer.Tick();

        if (health <= 0)
        {
            // Perform death logic here
            isDead = true;
            FinalBossManager.remaining--;
            animator.SetBool("playAttack", false);
            canTargetTakeDamage = false;
            canDealDamage = false;
            isAttacking = false;

            // Trigger death animation
            animator.SetTrigger(deathAnimationName);

            // Hide health bar
            healthBar.gameObject.SetActive(false);
        }

        // if the enemy is alive and is not currently taking damage
        if (!isDead && !isTakingDamage)
        {
            // find the current distance between the player target and the enemy
            float distance = Vector3.Distance(target.position, transform.position);

            // if the player target is within the look radius
            if (distance <= lookRadius)
            {
                timer = 0;

                // face the player target
                FaceTarget();

                // enable health bar
                healthBar.gameObject.SetActive(true);

                // get new rotation and apply it
                Vector3 currentRotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(currentRotation);

                // Randomly changes speed to a random value for a random amount of time
                if (!speedChanged)
                {
                    StartCoroutine(ChangeSpeed());
                }

                // if the enemy is not currently attacking
                if (!isAttacking)
                {
                    // if the distance between the enemy and the player target is less than or equal to the enemy stopping distance
                    if (distance <= agent.stoppingDistance)
                    {
                        // Stop walking animation and moving
                        animator.SetBool("IsWalking", false);
                        isMoving = false;
                    }
                    // otherwise
                    else
                    {
                        // Start walking animation (which starts moving)
                        animator.SetBool("IsWalking", true);

                        // once the enemy is "moving" then apply the actual moving
                        if (isMoving)
                        {
                            agent.SetDestination(target.position);
                        }

                    }
                }
            }
            // if the player target is not within the enemy look radius
            else
            {
                // disable health bar
                healthBar.gameObject.SetActive(false);

                // if the enemy should be wandering
                if (doesWander)
                {
                    // if the timer is up
                    if (timer >= wanderTimer)
                    {
                        // if not currently waiting
                        if (!isWaiting)
                        {
                            // reset wander timer
                            timer = 0;

                            // wait for a random amount of time
                            StartCoroutine(RandomWait());

                            // create a new location to travel to
                            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

                            // after waiting, move to the new random position
                            agent.SetDestination(newPos);

                            // play the walking animation and stop any attacking animation
                            animator.SetBool("IsWalking", true);
                            animator.SetBool("playAttack", false);

                            // get rotation of the new location
                            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                        }
                        else
                        {
                            isMoving = false;
                            animator.SetBool("IsWalking", false);
                            animator.SetBool("playAttack", false);
                        }
                    }
                    else
                    {
                        // the amount of time before the enemy moves on to a new random spot
                        timer += Time.deltaTime;
                    }
                }
                // if the enemy should not be wandering
                else
                {
                    // they have to remain still and stop playing an attack animation
                    animator.SetBool("IsWalking", false);
                    isMoving = false;
                    animator.SetBool("playAttack", false);
                    agent.SetDestination(agent.transform.position);
                }
            }
        }
        // if dead or taking damage (probably the latter)
        else
        {
            // face the target and apply the new rotation
            FaceTarget();

            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    // runs when the walking animation begins
    public void StartWalking()
    {
        isMoving = true;
    }

    // Randomly changes speed
    IEnumerator ChangeSpeed()
    {
        // changes enemy's speed with a random float between a min and max
        agent.speed = Random.Range(randomSpeedMin, randomSpeedMax);

        // only make the speed change once per coroutine call
        speedChanged = true;

        // change the animation speed relative to the speed of the spider
        animator.SetFloat(walkSpeedName, agent.speed / animationSpeedDivider);

        // wait for random number of time
        yield return new WaitForSeconds(Random.Range(randomWaitSpeedMin, randomWaitSpeedMax));

        // speed change is over
        speedChanged = false;
    }

    // waits for a random amount of time before picking a new random spot to wander
    IEnumerator RandomWait()
    {
        // only wait if not already waiting
        isWaiting = true;
        // make enemy stop moving and stop walking animation
        isMoving = false;
        animator.SetBool("IsWalking", false);

        // wait for the wanderTimer plus a random number
        yield return new WaitForSeconds(wanderTimer + Random.Range(4, 6));

        // enemy is done waiting
        isWaiting = false;
    }

    // faces the player target
    void FaceTarget()
    {
        curDirection = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(isNegative * curDirection.x, 0, isNegative * curDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // tries to attack the player object
    void TryAttack()
    {
        // if the player is in the hitbox, the enemy can currently deal damage, and the player is not rolling (gives i-frames)
        if (canTargetTakeDamage && canDealDamage && !playerManager.isRolling)
        {
            // now make player target take damage
            targetHealth.TakeDamage(damageDealt);

            // make target only take damage once per attack
            canDealDamage = false;
        }
    }

    // Method to handle taking damage from the player
    public void TakeDamage(int damageAmount)
    {
        if (!canBeDamaged) return;

        isTakingDamage = true;

        if (!isDead)
        {
            //Debug.Log("spider took " + damageAmount + " amount of damage");
            health -= damageAmount;

            healthSystem.AddToCurrentHealth(-damageAmount);
        }

        //Debug.Log("spider health after taking damage: " + health);

        // stop other animations and turn other booleans to false
        isMoving = false;
        animator.SetBool("IsWalking", false);

        if (health <= 0)
        {
            // Perform death logic here
            isDead = true;

            animator.SetBool("playAttack", false);
            canTargetTakeDamage = false;
            canDealDamage = false;
            isAttacking = false;

            // Trigger death animation
            animator.SetTrigger(deathAnimationName);

            // Hide health bar
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            // if not dead, just play take damage animation if staggered, otherwise do nothings

            // random chance stagger will interrupt attack animation
            int x = Random.Range(1, staggerChance + 1);

            if (x == staggerChance)
            {
                canBeStaggered = true;
            }
            else
            {
                canBeStaggered = false;
            }

            // currently isTakingDamage = true from above
            if (canBeStaggered)
            {
                animator.SetBool("takeDamage", true);
                animator.SetBool("playAttack", false);
                canTargetTakeDamage = false;
                canDealDamage = false;
                isAttacking = false;
            }

            canBeDamaged = false;

            damageCooldownTimer.StartTimer();
        }
    }


    // DEAL WITH PLAYER COLLISIONS

    // works with the player's character controller
    // once the player enters the hitbox
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            targetHealth = playerHealth;
            playerManager = collider.gameObject.GetComponent<PlayerManager>();
        }
    }

    // once the player leaves the hitbox
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerManager = collider.gameObject.GetComponent<PlayerManager>();

            // make it so that the player no longer has a chance to take damage as they are outside of the attack hitbox
            playerManager.canTakeDamage = false;
            canTargetTakeDamage = false;
        }
    }

    // if the player stays in the hitbox
    private void OnTriggerStay(Collider collider)
    {

        if (collider.gameObject.CompareTag("Particle"))
        {
            TakeDamage(40);
        }
        if (collider.gameObject.CompareTag("ParticleBlastDome"))
        {
            TakeDamage(100);
        }
        if (collider.gameObject.CompareTag("ParticleSlash"))
        {
            TakeDamage(80);
        }


        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = collider.gameObject.GetComponent<PlayerManager>();

            // try to attack (and damage) the player target
            TryAttack();

            // if currently taking damage and is staggered, stop all damage
            if (isTakingDamage && canBeStaggered)
            {
                playerManager.canTakeDamage = false;
                canTargetTakeDamage = false;
                canDealDamage = false;
            }

            // wait before attempting to try and attack if not already waiting
            else if (!isAttackWaiting)
            {


                // wait for a random amount of time before trying to attack
                last = StartCoroutine(WaitToAttack());

                if (isTakingDamage && canBeStaggered)
                {
                    playerManager.canTakeDamage = false;
                    canTargetTakeDamage = false;
                    canDealDamage = false;
                }
                else
                {
                    playerManager.canTakeDamage = true;
                    canTargetTakeDamage = true;

                    // pick randomly between attack animations and play it
                    int m = Random.Range(0, numberOfAttackAnimations);
                    animator.SetInteger("attackType", m);
                    animator.SetBool("playAttack", true);

                    // stop enemy from moving
                    isMoving = false;
                    animator.SetBool("IsWalking", false);
                }
            }
        }
    }

    // waits for a random amount of time
    IEnumerator WaitToAttack()
    {
        isAttackWaiting = true;
        float n = Random.Range(2, 3);
        yield return new WaitForSeconds(n);
        isAttackWaiting = false;
    }

    // ANIMATON EVENT FUNCTIONS

    // start of animation window of when the enemy can actually do damage
    public void Attack()
    {
        canDealDamage = true;
    }

    // end of this window ^
    public void EndAttack()
    {
        canDealDamage = false;
    }

    // beginning of attack animation
    public void StartAttack()
    {
        isAttacking = true;
        canDealDamage = false;
    }

    // end of attack animation
    public void ResetAttack()
    {
        // go back to idle
        animator.SetBool("playAttack", false);
        isAttacking = false;
        canDealDamage = false;
        //canBeDamaged = true;
    }

    // end of stagger (take damage) animation
    public void ResetTakeDamage()
    {
        // go back to idle
        animator.SetBool("takeDamage", false);
    }

    // end of death animation
    public void OnDeath()
    {
        // Disable further updates and interactions
        enabled = false;
        agent.enabled = false;
        Destroy(gameObject);

        StateManager.editItemCount(lootIndex, lootAmount);
        StateManager.editCurrency(currencyDrop);
        StateManager.questProgress[questIndex] += questAmount;
    }

    // ENEMY SOUND EFFECTS
    void PlaySpiderDamageSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.spiderDamageHitSFX1);
    }

    void PlaySpiderAttackSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.spiderAttackSFX1);
    }

    void PlaySpiderDeathSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.spiderDeathSFX1);
    }

    void PlayGolemDamageSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.golemDamageHitSFX1);
    }

    void PlayGolemAttackSoundFX()
    {
        // gives random int between 1 and 2
        int n = Random.Range(1, 3);
        if (n == 1)
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.golemAttackSFX1);
        }
        else
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.golemAttackSFX2);
        }
    }

    void PlayGolemDeathSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.golemDeathSFX1);
    }

    void PlayPyrothioAttackSoundFX()
    {

    }

    void PlayPyrothioDamageSFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.pyrothioDamageHitSFX1);
    }

    void PlayPyrothioDeathSoundFX()
    {

    }

    void PlayPyrothioStompSFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.pyrothioStompSFX1);
    }

    void PlayBanditDamageSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.banditDamageHitSFX);
    }

    void PlayBanditAttackSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.banditAttackSFX);
    }

    void PlayBanditDeathSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.banditDeathSFX);
    }

    void PlayKnightDamageSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.knightDamageHitSFX);
    }

    void PlayKnightAttackSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.knightAttackSFX);
    }

    void PlayKnightDeathSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.knightDeathSFX);
    }

    void PlayMetalonDamageSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.metalonDamageHitSFX);
    }

    void PlayMetalonAttackSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.metalonAttackSFX);
    }

    void PlayMetalonDeathSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.metalonDeathSFX);
    }

    // Draws look radius around enemy
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

