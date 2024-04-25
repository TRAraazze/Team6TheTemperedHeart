using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace CGP
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;
        public int swordSwingNum;
        public int swordSwingNumSaved;
        public int footstepNum;
        public int footstepNumSaved;
        public PlayerManager playerManager;
        

        protected virtual void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            audioSource = GetComponent<AudioSource>();
            swordSwingNum = UnityEngine.Random.Range(0, 3);
            footstepNum = UnityEngine.Random.Range(0, 3);
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
        }

        public void PlayJumpStartSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.jumpStartSFX);
        }

        public void PlayJumpLandSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.jumpLandSFX);
        }

        public void PlaySwordSwingSoundFX()
        {
            // we don't want to play the same audio clip in a row
            while (swordSwingNum == swordSwingNumSaved)
            {
                // get a random number between 0 and 2 (inclusive)
                swordSwingNum = UnityEngine.Random.Range(0, 3);
            }

            switch (swordSwingNum)
            {
                case 0:
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.swordSwingSFX1);
                    break;
                case 1: 
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.swordSwingSFX2);
                    break;
                case 2:
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.swordSwingSFX3);
                    break;
                default:
                    break;
            }

            // save value for next time
            swordSwingNumSaved = swordSwingNum;
        }
        
        public void PlayHeavySwordSwingSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.heavySwordSwingSFX);
        }

        public void PlayDefaultFootstepSoundFX(AnimationEvent evt)
        {

            if (evt.animatorClipInfo.weight <= 0.5f || playerManager.isPerformingAction)
                return;

            // we don't want to play the same audio clip in a row
            while (footstepNum == footstepNumSaved)
            {
                // get a random number between 0 and 2 (inclusive)
                footstepNum = UnityEngine.Random.Range(0, 3);
            }

            switch (footstepNum)
            {
                case 0:
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.defaultFootstepSFX1);
                    break;
                case 1:
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.defaultFootstepSFX2);
                    break;
                case 2:
                    audioSource.PlayOneShot(WorldSoundFXManager.instance.defaultFootstepSFX3);
                    break;
                default:
                    break;
            }

            // save value for next time
            footstepNumSaved = footstepNum;
            
        }

        public void PlayDamageSoundFX()
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
            
        }
    }
}
