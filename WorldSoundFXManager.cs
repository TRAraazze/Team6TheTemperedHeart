using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance;

        [Header("Action Sounds")]
        public AudioClip rollSFX;
        public AudioClip jumpStartSFX;
        public AudioClip jumpLandSFX;
        public AudioClip swordSwingSFX1;
        public AudioClip swordSwingSFX2;
        public AudioClip swordSwingSFX3;
        public AudioClip heavySwordSwingSFX;
        public AudioClip defaultFootstepSFX1;
        public AudioClip defaultFootstepSFX2;
        public AudioClip defaultFootstepSFX3;
        public AudioClip damageSFX1;
        public AudioClip damageSFX2;
        public AudioClip spiderDamageHitSFX1;
        public AudioClip spiderAttackSFX1;
        public AudioClip spiderDeathSFX1;
        public AudioClip golemAttackSFX1;
        public AudioClip golemAttackSFX2;
        public AudioClip golemDamageHitSFX1;
        public AudioClip golemDeathSFX1;
        public AudioClip pyrothioAttackSFX1;
        public AudioClip pyrothioAttackSFX2;
        public AudioClip pyrothioDamageHitSFX1;
        public AudioClip pyrothioDeathSFX1;
        public AudioClip pyrothioStompSFX1;
        public AudioClip banditDamageHitSFX;
        public AudioClip banditAttackSFX;
        public AudioClip banditDeathSFX;
        public AudioClip metalonDamageHitSFX;
        public AudioClip metalonAttackSFX;
        public AudioClip metalonDeathSFX;
        public AudioClip knightDamageHitSFX;
        public AudioClip knightAttackSFX;
        public AudioClip knightDeathSFX;
        public AudioClip buyDeclineSFX;
        public AudioClip buySuccessSFX;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
