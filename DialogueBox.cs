using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CGP
{
    public class DialogueBox : MonoBehaviour
    {
       public TextMeshProUGUI dialogueText;
       public TextMeshProUGUI nameText;
       public GameObject dialoguePanel;
       void Start()
       {
           dialoguePanel.SetActive(false);
       } 
      
        public void ShowDialogue(string dialogue, string speaker)
        {
            nameText.text = speaker + ":";
            dialogueText.text = dialogue;
            dialoguePanel.SetActive(true);
        }

        public void ShowDialogue(string dialogue, string speaker, bool hasColon)
        {
            nameText.text = speaker;
            dialogueText.text = dialogue;
            dialoguePanel.SetActive(true);
        }

        public void EndDialogue()
        {
            nameText.text = null;
            dialogueText.text = null;;
            dialoguePanel.SetActive(false);
        }
    }
}
