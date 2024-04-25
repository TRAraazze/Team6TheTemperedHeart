using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CGP
{
    public class StateManager : MonoBehaviour
    {
        public static bool isFireUnlocked = false;
        public static bool isIceUnlocked = false;
        public static bool isAirUnlocked = false; 

        public static bool[] questDialogue = new bool[22];
        public static bool[] questComplete = new bool[22];
        public static int[] questProgress = new int[22];
        public static bool[] hasQuest = new bool [22];
        
        public static int currency = 0;

        public static double karma = 0.5;

        public static int[] inventory = new int[12];
        public static int spawn = 0;
        
        public static void editItemCount(int ID, int diff)
        {
            inventory[ID] += diff;
        }

        public static void editCurrency(int diff)
        {
            currency += diff;
        }

        public static void editKarma(double diff)
        {
            karma += diff;
        }
    }
}




