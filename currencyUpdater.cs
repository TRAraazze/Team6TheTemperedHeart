using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CGP
{
    public class currencyUpdater : MonoBehaviour
    {
        public TextMeshProUGUI currencyText;
        
        // Start is called before the first frame update
        void Start()
        {
            currencyText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            currencyText.text = StateManager.currency.ToString();
        }
    }
}
