using CGP;
using UnityEngine;
using UnityEngine.AI;

// removing this script as it was messing up collisions plus redundant, check out EnemyController.cs
/*
public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public Animator animator; // Reference to the Animator component
    public string walkAnimationName = "walk"; // Name of the walk animation
    public string idleAnimationName = "idle"; // Name of the idle animation
    public string[] attackAnimationNames = { "attack1", "attack2" }; // Names of the attack animations
    public string damageAnimationName = "takeDamage"; // Name of the damage animation
    public string deathAnimationName = "death"; // Name of the death animation

    [Header("Enemy Stats")]
    public int damageDealt = 100;
    public int health = 1000;
    public bool isDead = false;
    public bool isAttacking = false;

    // target is the player
    Transform target;
    NavMeshAgent agent;

    int currentAttackIndex = 0;

    void Start()
    {
        target = PlayerSceneManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        // Start with idle animation
        animator.SetTrigger(idleAnimationName);
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

                // Add 180 degrees to the Y rotation
                currentRotation.y += 180f;

                // Apply the new rotation
                transform.rotation = Quaternion.Euler(currentRotation);
                agent.SetDestination(target.position);

                if (!isAttacking)
                {
                    Attack();
                }

                if (distance <= agent.stoppingDistance + 1.5f) // Adjusted stopping distance
                {
                    // Stop walking
                    animator.ResetTrigger(walkAnimationName);
                }
                else
                {
                    // Start walking
                    animator.SetTrigger(walkAnimationName);
                }
            }
            else
            {
                // Stop walking and attacking when target is out of range
                animator.ResetTrigger(walkAnimationName);
                animator.ResetTrigger(attackAnimationNames[currentAttackIndex]);
                // Trigger idle animation
                animator.SetTrigger(idleAnimationName);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Attack()
    {
        // Trigger attack animation
        animator.SetTrigger(attackAnimationNames[currentAttackIndex]);
        isAttacking = true;

        // Switch to the next attack animation for the next attack
        currentAttackIndex = (currentAttackIndex + 1) % attackAnimationNames.Length;

        // Delay before resetting attack
        Invoke("ResetAttack", animator.GetCurrentAnimatorClipInfo(0).Length);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
            health -= damageAmount;

        Debug.Log("spider health after taking damage: " + health);

        if (health <= 0 && !isDead)
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
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // works with the player's character controller
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            Debug.Log(playerHealth == null);
            playerHealth.TakeDamage(100);
        }
    }
}
    */