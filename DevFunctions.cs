using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class DevFunctions : MonoBehaviour
    {
        // Start is called before the first frame update
        public void EnableDialogue()
        {
            for(int i = 0; i < StateManager.questDialogue.Length; i++)
            {
                StateManager.questDialogue[i] = true;
            }
        }
    }
}
