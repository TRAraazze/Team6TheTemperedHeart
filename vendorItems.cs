using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static vendor; 


public class vendorItems : MonoBehaviour
{
    public TMP_Text itemOneText;
    public TMP_Text itemOneDescription;


    // Start is called before the first frame update
    void Start()
    {

        itemOneText.text = "TitleOne";
        itemOneDescription.text = "DescOne";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
         
    }
}

