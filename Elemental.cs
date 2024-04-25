using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class Elemental : MonoBehaviour
    {
        // Reference to PlayerInputManager
        public PlayerInputManager playerInputManager;




        // References to buttons
        public Button fireButton;
        public Button iceButton;
        public Button airButton;
        GameObject fireCase;
        GameObject fire;
        GameObject airCase;
        GameObject air;
        GameObject iceCase;
        GameObject ice;
        // Reference to the SkillTree script
        public SkillTree skillTree;

        // Start is called before the first frame update
        void Start()
        {
            playerInputManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
            // Add the method OnFireButtonClick as a listener to the fire button's OnClick event
            fireButton.onClick.AddListener(OnFireButtonClick);
            iceButton.onClick.AddListener(OnIceButtonClick);
            airButton.onClick.AddListener(OnAirButtonClick);
            fireCase = GameObject.Find("FireCase");
            fire = GameObject.Find("Fire");
            airCase = GameObject.Find("AirCase");
            air = GameObject.Find("Air");
            iceCase = GameObject.Find("IceCase");
            ice = GameObject.Find("Ice");
            fireCase.SetActive(false);
            fire.SetActive(false);
            iceCase.SetActive(false);
            ice.SetActive(false);
            airCase.SetActive(false);
            air.SetActive(false);
        }

        void Update()
        {

            if (StateManager.isFireUnlocked)
            {
                fireCase.SetActive(true);
                fire.SetActive(true);
            }


            if (StateManager.isAirUnlocked)
            {
                airCase.SetActive(true);
                air.SetActive(true);
            }


            if (StateManager.isIceUnlocked)
            {
                iceCase.SetActive(true);
                ice.SetActive(true);
            }



        }

        // This method is called when the fire button is clicked
        public void OnFireButtonClick()

        {

            Debug.Log("FIRE");

            if (skillTree != null)
            {
                // Call a method in SkillTree to find out which fire skill is active
                int activeFireSkill = skillTree.GetFireValue();

                // Log the active fire skill
                Debug.Log("Active Fire Skill: " + activeFireSkill);

                // You can also use the active fire skill to change the game state
                // depending on your game logic
                // For example:
                if (activeFireSkill == 1)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Fire1);
                }
                else if (activeFireSkill == 2)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Fire2);
                }
                else if (activeFireSkill == 3)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Fire3);
                }
            }
            else
            {
                Debug.LogWarning("SkillTree reference is not assigned.");
            }
        }

        public void OnAirButtonClick()
        {

            Debug.Log("Air");

            if (skillTree != null)
            {
                // Call a method in SkillTree to find out which fire skill is active
                int activeAirSkill = skillTree.GetAirValue();

                // Log the active fire skill
                Debug.Log("Active Fire Skill: " + activeAirSkill);

                // You can also use the active fire skill to change the game state
                // depending on your game logic
                // For example:
                if (activeAirSkill == 1)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Air1);
                }
                else if (activeAirSkill == 2)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Air2);
                }
                else if (activeAirSkill == 3)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Air3);
                }
                }
                else
                {
                    Debug.LogWarning("SkillTree reference is not assigned.");
                }
                
            }

        public void OnIceButtonClick()
        {
            Debug.Log("ICE");

            if (skillTree != null)
            {
                // Call a method in SkillTree to find out which fire skill is active
                int activeIceSkill = skillTree.GetIceValue();

                // Log the active fire skill
                Debug.Log("Active Fire Skill: " + activeIceSkill);

                // You can also use the active fire skill to change the game state
                // depending on your game logic
                // For example:
                if (activeIceSkill == 1)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Ice1);
                }
                else if (activeIceSkill == 2)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Ice2);
                }
                else if (activeIceSkill == 3)
                {
                    playerInputManager.SetGameState(PlayerInputManager.GameState.Ice3);
                }
            }
            else
            {
                Debug.LogWarning("SkillTree reference is not assigned.");
            }
        }

        // Add other button click event handler methods for ice and air as needed.
    }
}
