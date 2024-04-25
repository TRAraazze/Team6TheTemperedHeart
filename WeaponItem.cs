using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class WeaponItem : Item
    {
        // for the future maybe put an animator controller override so that
        // the attack animations change based on which weapon you are holding

        public PlayerWeaponManager weaponManager;
        PlayerManager player;
        PlayerInputManager inputManager;

        [Header("Weapon Model")]
        public GameObject weaponModel;
        public GameObject weaponModelClone;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 100;
        public int fireDamage = 0;
        public int iceDamage = 0;
        public int lightningDamage = 0;

        // WEAPON MODIFIERS
        // LIGHT ATTACK MODIFIER
        // HEAVY ATTACK MODIFIER
        // CRITICAL DAMAGE MODIFIER ETC
        public int heavyAttackModifier = 4;

        public int totalDamage;

        //[Header("Stamina Costs")]
        //public int baseStaminaCost = 20;
        // RUNNING ATTACK STAMINA COST MODIFIER
        // LIGHT ATTACK STAMINA COST MODIFIER
        // HEAVY ATTACK STAMINA COST MODIFIER

        // WEAPON "DEFLECTION" (if the weapon will bounce off another weapon when it is being guarded against) STRETCH GOAL ONLY FOR MELEE WEAPONS

        // ITEM BASED ACTIONS

        // BLOCKING SOUNDS

        private void Awake()
        {
            player = GameObject.Find("UniversalCharacter").GetComponent<PlayerManager>();
            inputManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
        }

        private void Update()
        {
            if (inputManager.heavyAttackInput)
            {
                totalDamage = physicalDamage * heavyAttackModifier;
            }
            else
            {
                totalDamage = physicalDamage;
            }
        }
    }
}
