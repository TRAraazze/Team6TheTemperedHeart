using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace CGP
{
    public class PotionCounter : MonoBehaviour
    {
        public static int potionNum;
        public TMP_Text textMesh;
        public static HealthSystemForDummies healthSystem;
        static float healAmount = 500f;

        // Start is called before the first frame update
        void Start()
        {
            healthSystem = GameObject.Find("HUD").GetComponentInChildren<HealthSystemForDummies>();
            textMesh = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            potionNum = StateManager.inventory[2]; // gets the current number of health potions in inventory from the state manager
            textMesh.text = "" + potionNum;
        }

        static public void DecreaseHealthPotions()
        {
            if (healthSystem.CurrentHealth < healthSystem.MaximumHealth && StateManager.inventory[2] > 0)
            {
                //Debug.Log("HEALING");
                healthSystem.AddToCurrentHealth(healAmount);
                StateManager.inventory[2]--;
                potionNum -= 1;
            }

        }
    }
}
