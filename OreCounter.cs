using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class OreCounter : MonoBehaviour
{
    public static int OreUsed = 0; 
    public static int OreTotal = 3;
    public TMP_Text oreDisplay;
    public static OreCounter oreCounter;
    private void Awake() => oreCounter = this;


    // Start is called before the first frame update
    void Start()
    {
        oreDisplay.text = "Ore: " + OreUsed +  "/" + OreTotal;
    }

    // Update is called once per frame
    void Update()
    {
        oreDisplay.text = "Ore: " + OreUsed + "/" + OreTotal;
    }

    public static bool AddOre()
    {

        //Debug.Log("Hi");

        if (OreUsed < OreTotal)
        {
            OreUsed += 1;
            return true;
        } else
        {
            return false;
        }
    }

    public static bool DecrementOre()
    {
        if (OreUsed > 0)
        {
            OreUsed--;
            return true;
        }
        else
        {
            return false;
        }
    }

}

