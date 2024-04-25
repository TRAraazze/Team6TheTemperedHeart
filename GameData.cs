using UnityEngine;


[System.Serializable]
public class GameData
{
    public bool[] questDialogue;
    public bool[] questComplete;
    public bool[] hasQuest;
    public int[] questProgress;
    public int currency;
    public double karma;
    public int[] inventory;
    public int spawn;
    public string sceneName;

    public bool isFireUnlocked;
    public bool isIceUnlocked;
    public bool isAirUnlocked; 

    // Add more fields as needed
}
