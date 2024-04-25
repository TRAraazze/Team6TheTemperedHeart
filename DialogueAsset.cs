using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    [CreateAssetMenu]
    public class DialogueAsset : ScriptableObject
    {
        [TextArea]
        public string[] dialogue;
    }
}
