using DevionGames;
using UnityEngine;

namespace CGP
{
    public class PlayerManager : CharacterManager
    {

        //public GameObject attackTransform;
        public GameObject playerObject;
        public int spawnNum;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerSoundFXManager playerSoundFXManager;
        public ItemHitbox hitbox;

        protected override void Awake()
        {
            base.Awake();

            Cursor.visible = false;

            // DO MORE STUFF, ONLY FOR THE PLAYER
            playerObject = GameObject.Find("UniversalCharacter");
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        protected override void Update()
        {
            base.Update();

            // HANDLE MOVEMENT EVERY FRAME
            playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            
            PlayerCamera.instance.HandleAllCameraActions();
        }

        protected override void Start()
        {
            // teleports player to correct spawn location

            if (SpawnManager.spawnCoords[PlayerPrefs.GetInt("spawnNum", 0)] == Vector3.zero)
            {
                playerObject.transform.position = SpawnManager.spawnCoords[0];
            }
            else
            {
                playerObject.transform.position = SpawnManager.spawnCoords[PlayerPrefs.GetInt("spawnNum", 0)];
            }

            

            if (StateManager.inventory[1] != 1)
                StateManager.editItemCount(1, 1);

            base.Start();
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;

            //hitbox = playerObject.GetComponentInChildren<ItemHitbox>();

            if (hitbox != null )
            {
                hitbox.gameObject.SetActive(false);
            }
        }
        
    }
}

