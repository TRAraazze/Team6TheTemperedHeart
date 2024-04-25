using CGP;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class BossController : MonoBehaviour
{
    public float lookRadius = 5f;
    public Animator animator; // Reference to the Animator component
    public string walkAnimationName = "Walk Forward"; // Name of the walk animation
    public string idleAnimationName = "idle"; // Name of the idle animation
    //public string[] attackAnimationNames = { "attack1", "attack2" }; // Names of the attack animations
    public string attackAnimationName = "Stab Attack";
    public string damageAnimationName = "Take Damage"; // Name of the damage animation
    public string deathAnimationName = "death"; // Name of the death animation

    [Header("Enemy Stats")]
    public int damageDealt = 300;
    public int health = 2000;
    public bool isDead = false;
    public bool isAttacking = false;
    public bool canBeDamaged = true;
    public bool isMoving = false;

    // target is the player
    Transform target;
    NavMeshAgent agent;

    public Timer damageCooldownTimer;
    public float damageCooldownAmount = 0.5f;

    public GameObject playerTarget;

    int currentAttackIndex = 0;

    void Start()
    {
        //target = PlayerSceneManager.instance.player.transform;
        target = playerTarget.transform;
        Debug.Log("target name: " + target.name);

        agent = GetComponent<NavMeshAgent>();

        // Start with idle animation
        //animator.SetTrigger(idleAnimationName);

        damageCooldownTimer = new Timer(damageCooldownAmount);
    }

    void Update()
    {
        if (!isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                FaceTarget();
                Vector3 currentRotation = transform.rotation.eulerAngles;

                // Apply the new rotation
                transform.rotation = Quaternion.Euler(currentRotation);

               
                agent.SetDestination(target.position);


                if (distance <= agent.stoppingDistance)
                {
                    // Stop walking
                    isMoving = false;
                    //animator.ResetTrigger(walkAnimationName);
                    animator.SetBool(walkAnimationName, isMoving);
                }
                else
                {
                    // Start walking
                    isMoving = true;
                    //animator.SetTrigger(walkAnimationName);
                    animator.SetBool(walkAnimationName, isMoving);
                }

            }
            else
            {
                // Stop walking and attacking when target is out of range
                //animator.ResetTrigger(walkAnimationName);
                // animator.ResetTrigger(attackAnimationNames[currentAttackIndex]);
                animator.ResetTrigger(attackAnimationName);
                // Trigger idle animation
                //animator.SetTrigger(idleAnimationName);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Attack(PlayerHealth playerHealth)
    {
        //Debug.Log("spider is attacking");

        // Trigger attack animation
        //animator.SetTrigger(attackAnimationNames[currentAttackIndex]);
        animator.SetTrigger(attackAnimationName);
        isAttacking = true;

        //Invoke("DamagePlayer", animator.GetCurrentAnimatorStateInfo(0).normalizedTime/3);
        StartCoroutine(DamagePlayer(playerHealth));

        // Delay before resetting attack
        Invoke("ResetAttack", animator.GetCurrentAnimatorClipInfo(0).Length);
        //animator.ResetTrigger(attackAnimationNames[currentAttackIndex]);

        // Switch to the next attack animation for the next attack
        //currentAttackIndex = (currentAttackIndex + 1) % attackAnimationNames.Length;

    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    IEnumerator DamagePlayer(PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).normalizedTime/3);
        playerHealth.TakeDamage(damageDealt);
    }

    IEnumerator DamageCooldown()
    {
        Debug.Log("inside coroutine");
        yield return new WaitForSeconds(damageCooldownAmount);
        canBeDamaged = true;
        Debug.Log("finished coroutine, canBeDamaged: " + canBeDamaged);
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("canBeDamaged: " + canBeDamaged);
        if (!canBeDamaged) return;

        if (!isDead)
        {
            Debug.Log("spider took " + damageAmount + " amount of damage");
            health -= damageAmount;
        }

        Debug.Log("spider health after taking damage: " + health);

        if (health <= 0)
        {
            isDead = true;
            // Perform death logic here

            // Trigger death animation
            animator.SetTrigger(deathAnimationName);

            // Disable further updates and interactions
            enabled = false;
            agent.enabled = false;
        }
        else // if not dead, just play take damage animation
        {
            animator.SetTrigger(damageAnimationName);
            canBeDamaged = false;
            StartCoroutine(DamageCooldown());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    /*
    // works with the player's character controller
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && )
        {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            Attack();
            playerHealth.TakeDamage(100);
        }
    }
    */

    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            Attack(playerHealth);
        }
    }
    
}
