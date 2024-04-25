using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class PlayerEventManager : MonoBehaviour
    {
        PlayerManager playerManager;

        private void Start()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        // make hitbox only active when player is attacking (and at a specific point when attacking)
        public void MakeHitboxActive()
        {
            playerManager.hitbox.gameObject.SetActive(true);
        }

        public void MakeHitboxInactive()
        {
            playerManager.hitbox.gameObject.SetActive(false);
        }
    }
}
