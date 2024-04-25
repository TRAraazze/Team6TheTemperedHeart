using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace CGP
{
    public class FinalBossManager : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        public GameObject portal;
        public GameObject portalEffect;

        public GameObject[] enemies;
        public float spawnRadius;
        public static int remaining = 0;
        public static bool defeated = false;

        private PlayerManager player;
        private PlayerInputManager playerInputManager;
        private PlayerControls playerControls;

        private bool introShown = false;
        public int enemySpawnNum;

        public Animator fatherAnimator;
        public AudioSource playerAudio;

        // Start is called before the first frame update
        void Start()
        {
            fatherAnimator = GameObject.Find("EvilFather").GetComponent<Animator>();
            player = GameObject.Find("UniversalCharacter").GetComponent<PlayerManager>();
            playerAudio = player.GetComponent<AudioSource>();
            playerInputManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
            playerControls = playerInputManager.playerControls;

            // call function that starts intro dialogue from final boss
            interactionCount = 0;
            infoTextCanvas.alpha = 1;
            infoTextCanvas.blocksRaycasts = true;

            StartCoroutine(WaitForIntro());

            OnInteraction();
        }

        IEnumerator WaitForIntro()
        {
            yield return new WaitForSeconds(2);
        }

        // Update is called once per frame
        void Update()
        {
            if (!introShown)
            {
                player.canMove = false;
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, 0, false);
                playerControls.PlayerActions.Disable();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnInteraction();
                }

            }

            if (MonsterSpawner.Destroyed >= 3)
            {
                MonsterSpawner.Destroyed -= 1000;
                portal.SetActive(true);
                portalEffect.SetActive(true);
                defeated = true;
                fatherAnimator.SetTrigger("death");
                playerAudio.PlayOneShot(WorldSoundFXManager.instance.banditDeathSFX);
                StateManager.questComplete[20] = true;
                StateManager.questDialogue[21] = false;
            }
        }

        // intro dialogue explaining the final reveal
        private void OnInteraction()
        {
            
            switch (interactionCount)
            {
                case (0):
                    _dialogueBox.ShowDialogue("Well, well, well...you've finally arrived. I have been anticipating you.", "Unknown");
                    StateManager.questComplete[19] = true;
                    StateManager.questDialogue[20] = false;
                    interactionCount++;
                    break;
                case (1):
                    _dialogueBox.ShowDialogue("You look confused and...angry? Why, don't you know who I am?", "Unknown");
                    interactionCount++;
                    break;
                case (2):
                    _dialogueBox.ShowDialogue("Of course I do. You're the leader of Titania, the same one who invaded my homeland and killed my father!", "You");
                    interactionCount++;
                    break;
                case (3):
                    _dialogueBox.ShowDialogue("Now, now, no need to get so worked up... may I impart upon you the truth?", "Unknown");
                    interactionCount++;
                    break;
                case (4):
                    _dialogueBox.ShowDialogue("I used to live in Aurum many years ago. The people of the land loved me and looked to me for help and guidance.", "Unknown");
                    interactionCount++;
                    break;
                case (5):
                    _dialogueBox.ShowDialogue("I always lended a hand and every time I aided someone, it just felt so...right. I couldn't get enough.", "Unknown");
                    interactionCount++;
                    break;
                case (6):
                    _dialogueBox.ShowDialogue("Soon, others noticed how eager I was to help people, so they started to come to me less and less for help.", "Unknown");
                    interactionCount++; 
                    break;
                case (7):
                    _dialogueBox.ShowDialogue("I couldn't bear this. I <b>needed</b> to help. So I started forcing my help unto those who I saw needed it.", "Unknown");
                    interactionCount++;
                    break;
                case (8):
                    _dialogueBox.ShowDialogue("But nobody liked this one bit. They were so ungrateful to me. Why wouldn't they submit? It made no sense.", "Unknown");
                    interactionCount++;
                    break;
                case (9):
                    _dialogueBox.ShowDialogue("Rumors started to spread about me... lies upon lies floated about like a displeasant fog. The elders of Aurum decided to exile me.", "Unknown");
                    interactionCount++;
                    break;
                case (10):
                    _dialogueBox.ShowDialogue("This would not do. I caught wind of this plan and decided to carry out one of my own first. I constructed a portal from Aurum to Titania.", "Unknown");
                    interactionCount++;
                    break;
                case (11):
                    _dialogueBox.ShowDialogue("I used my power to transform the men and creatures I found into corrupted peons who would obey my every command.", "Unknown");
                    interactionCount++;
                    break;
                case (12):
                    _dialogueBox.ShowDialogue("I now had the perfect means to help the whole world. Everyone needs me. Whether they want it or not.", "Unknown");
                    interactionCount++;
                    break;
                case (13):
                    _dialogueBox.ShowDialogue("Using force, I descended upon Aurum and pillaged it, killing anybody who opposed me. Those who fight against my help do not deserve to live.", "Unknown");
                    interactionCount++;
                    break;
                case (14):
                    _dialogueBox.ShowDialogue("I then left, my task complete. I left my creatures back on Aurum as a reminder to those who refuse my aid.", "Unknown");
                    interactionCount++;
                    break;
                case (15):
                    _dialogueBox.ShowDialogue("Why have you told me all of this? You're still a villain. You murdered thousands of innocent lives.", "You");
                    interactionCount++;
                    break;
                case (16):
                    _dialogueBox.ShowDialogue("Well, I knew I wouldn't convince you so easily... I mainly told you so that you may understand me better.", "Unknown");
                    interactionCount++;
                    break;
                case (17):
                    _dialogueBox.ShowDialogue("I think it's only fair that you know more about... <b>your own father</b>.", "Unknown");
                    interactionCount++;
                    break;
                case (18):
                    _dialogueBox.ShowDialogue("What's that look of shock on your face? Didn't expect that, did you? Unfortunately we don't have time for a father son reunion.", "Your Father");
                    interactionCount++;
                    break;
                case (19):
                    _dialogueBox.ShowDialogue("I will use my powers, strengthened by these totems, to call upon my creatures and men to destroy you. I'm proud of you for getting this far.", "Your Father");
                    interactionCount++;
                    break;
                case (20):
                    _dialogueBox.ShowDialogue("You really are your father's son... farewell... ARISE MY PEONS!!", "Your Father");
                    interactionCount++;
                    break;
                case (21):
                    _dialogueBox.EndDialogue();
                    interactionCount = 0;
                    infoTextCanvas.alpha = 0;
                    infoTextCanvas.blocksRaycasts = false;
                    introShown = true;

                    if (!player.isPerformingAction)
                        player.canMove = true;

                    playerControls.PlayerActions.Enable();
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    remaining = 0;
                    SpawnEnemies();
                    // call function to spawn enemies
                    break;
                default:
                    Debug.Log("something went wrong");
                    break;
            }
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
                remaining++;
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
    }
}
