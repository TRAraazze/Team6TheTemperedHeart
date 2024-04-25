using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGP
{
    public class Item : MonoBehaviour
    {
        [Header("Item Information")]
        public string itemName;
        public Sprite itemIcon;
        [TextArea] public string itemDescription;
        public int itemID;
        
    }
}
