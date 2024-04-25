using DevionGames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CGP
{
    public class ItemHitbox : MonoBehaviour
    {
        WeaponItem weapon;
        public int ignoreLayer1 = 6;
        public int ignoreLayer2 = 12;
        PlayerInputManager playerInput;
        PlayerManager playerManager;
        GameObject player;

        private Vector3 moveDirection;

        private void Start()
        {
            weapon = GetComponentInParent<WeaponItem>();

            // Ignores collisions with the player
            Physics.IgnoreLayerCollision(ignoreLayer1, ignoreLayer2);

            playerInput = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
            player = GameObject.Find("UniversalCharacter");
            playerManager = player.GetComponent<PlayerManager>();
        }
        
        // HANDLE COLLISIONS

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("enemy collision");
                // only let the collision do anything if the player is actively attacking
                if (playerInput.lightAttackInput || playerInput.heavyAttackInput)
                {
                    Debug.Log("Item collision with enemy while player is attacking");
                    EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
                    enemyController.TakeDamage(weapon.totalDamage);
                }
                
            }
        }
    }
}
