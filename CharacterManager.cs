using UnityEngine;


namespace CGP
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isSprinting = false;
        public bool isJumping = false;
        public bool isRolling = false;
        public bool isGrounded = true;
        public bool isTwoHanded = true;
        public bool isMoving = false;
        public bool isAttacking = false;
        public bool isAlive = true;
        public bool canTakeDamage = false;
        public bool isTakingDamage = false;

        [Header("Attacking")]
        [SerializeField] public int numberAttacks = 3;
        [SerializeField] public int currentAttackCounter = 0; // STARTS AT 0 BECAUSE NOT ATTACKING
        [SerializeField] private float attackCounterResetCooldown = 2;
        public Timer attackCounterResetTimer;

        [Header("Stamina")]
        [SerializeField] public float maxStamina = 100f;
        [SerializeField] public float currentStamina;

        protected virtual void Awake()
        {
            //DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void ResetAttackCounter() => currentAttackCounter = 0;

        private void OnEnable()
        {
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
        
        // runs every frame
        protected virtual void Update()
        {
            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("isTwoHanded", isTwoHanded);
            animator.SetBool("isMoving", isMoving);
            animator.SetInteger("AttackCounter", currentAttackCounter);
        }

        protected virtual void LateUpdate()
        {

        }
        protected virtual void Start()
        {
            
        }
    }
}

