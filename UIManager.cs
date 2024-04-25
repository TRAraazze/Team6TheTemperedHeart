using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questInfoText; // Reference to the TextMeshProUGUI component displaying quest information

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DisplayQuestName(string questName)
    {
        questNameText.text = questName;
    }
    // Function to display quest information
    public void DisplayQuestInfo(string questDescription)
    {
        questInfoText.text = questDescription;
    }
}
