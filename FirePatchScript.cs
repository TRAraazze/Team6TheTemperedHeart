using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class FirePatchScript : MonoBehaviour
    {
        public GameObject patch;
        public ParticleSystem firepatch1;
        public ParticleSystem firepatch2;
        public ParticleSystem firepatch3;
        private bool isBurning = false;
        private float interval = 10f;
        private float timePassed = 0f;
        private float burntime = 0f;
        private float burninterval = 0.75f;

        public static int dps = 5;
        // Start is called before the first frame update
        void Start()
        {
            if (Random.Range(0.0f, 1.0f) > 0.5)
            {
                isBurning = true;
                var main = firepatch1.main;
                Debug.Log("making red");
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
                main = firepatch2.main;
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
                main = firepatch3.main;
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
            }
            else
            {
                isBurning = false;
                var main = firepatch1.main;
                main.startColor = Color.gray;
                main = firepatch2.main;
                main.startColor = Color.gray;
                main = firepatch3.main;
                main.startColor = Color.gray;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= interval)
            {
                timePassed = 0;
                swap();
            }
        }

        public void swap()
        {
            if (Random.Range(0.0f, 1.0f) > 0.5)
            {
                isBurning = true;
                Debug.Log("making red");
                var main = firepatch1.main;
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
                main = firepatch2.main;
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
                main = firepatch3.main;
                main.startColor = new Color((190f/255f), (52f/255f), (44f/255f), 0.7f);
            }
            else
            {
                isBurning = false;
                var main = firepatch1.main;
                Debug.Log("making gray");
                main.startColor = Color.gray;
                main = firepatch2.main;
                main.startColor = Color.gray;
                main = firepatch3.main;
                main.startColor = Color.gray;
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player is inside the collider, enable interaction
                burntime += Time.deltaTime;
                if (burntime >= burninterval && isBurning)
                {
                    
                }
            }
        }
    }
}
