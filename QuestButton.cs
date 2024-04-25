using TMPro;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI questNameText; // Reference to the TextMeshProUGUI component displaying quest name
    public string questDescription; // Description of the quest

    // Called when the quest button is clicked
    public void OnClick()
    {
        // Display quest information
        UIManager.Instance.DisplayQuestInfo(questDescription);
    }
}
