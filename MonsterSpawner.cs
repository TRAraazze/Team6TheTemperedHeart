using System.Data.Common;
using CGP;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class MonsterSpawner : MonoBehaviour
    {
        private bool isInteractable = false;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        public CanvasGroup infoTextCanvas;
        public GameObject orePiece;
        public static int Destroyed = 0;

        public GameObject[] enemies;
        public int enemySpawnNum;
        public int spawnRadius;

        // Function to call when interacting
        void OnInteraction()
        {
            // Implement your interaction logic here


            SpawnEnemies();

            isInteractable = false;
            infoText.text = null;
            infoTextCanvas.alpha = 0; //this makes everything transparent
            infoTextCanvas.blocksRaycasts = false;
            orePiece.SetActive(false);
            Destroyed++;
        }

        private void SpawnEnemies()
        {
            //Instantiate(enemies[Random.Range(0, enemies.Length - 1)]);
            for (int i = 0; i < enemySpawnNum; i++)
            {
                // gets spawn position
                Vector3 spawnPos = RandomPointOnCircleEdge(spawnRadius);

                // spawns enemy
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length - 1)], spawnPos, Quaternion.identity) as GameObject;

                // adjust height
                enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
                FinalBossManager.remaining++;
            }
        }

        private Vector3 RandomPointOnCircleEdge(float radius)
        {
            bool correctSpawn = false;

            Vector2 vector2 = new Vector2(0, 0);

            // number of times we will try to search for an empty position
            int searchCount = 10;

            while (searchCount-- > 0 && !correctSpawn)
            {
                vector2 = Random.insideUnitCircle.normalized * radius;
                vector2.x += transform.position.x;
                vector2.y += transform.position.z;

                correctSpawn = CheckIfPositionIsOccupied(vector2);
            }

            return new Vector3(vector2.x, transform.position.y, vector2.y);
        }

        private static bool CheckIfPositionIsOccupied(Vector2 vector2)
        {
            bool correctSpawn = true;

            Collider[] collidersDetected = Physics.OverlapBox(new Vector3(vector2.x, 0, vector2.y), new Vector3(1, 2, 1f));

            if (collidersDetected.Length != 0)
            {
                foreach (Collider col in collidersDetected)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        correctSpawn = false;
                    }
                }
            }

            return correctSpawn;
        }


        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player is inside the collider, enable interaction
                EnableInteraction();
                isInteractable = true;
                infoText.text = "Press E to Destroy";
                infoTextCanvas.alpha = 1; //this makes everything transparent
                infoTextCanvas.blocksRaycasts = true; //this prevents the UI element to receive input events
            }
        }

        // Check for player exiting the collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player has exited the collider, disable interaction
                DisableInteraction();
                isInteractable = false;
                infoText.text = null;
                infoTextCanvas.alpha = 0; //this makes everything transparent
                infoTextCanvas.blocksRaycasts = false; //this prevents the UI element to receive input events
            }
        }

        // Enable interaction
        void EnableInteraction()
        {
            // Subscribe to the key press event
            if (!Input.GetKeyDown(KeyCode.E))
            {
                Input.GetKeyDown(KeyCode.E);
            }
        }

        // Disable interaction
        void DisableInteraction()
        {
            // Unsubscribe from the key press event
            if (Input.GetKeyDown(KeyCode.E))
            {
                Input.GetKeyUp(KeyCode.E);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check for 'E' key press while player is inside collider
            if (Input.GetKeyDown(KeyCode.E) && isInteractable)
            {
                OnInteraction();
            }
        }

        void Start()
        {
            infoTextCanvas.alpha = 0; //this makes everything transparent
            infoTextCanvas.blocksRaycasts = false; //this prevents the UI element to receive input events
        }
    }
}
