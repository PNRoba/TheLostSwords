using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position = new float[3];

    public string[] questNames;
    public bool[] ifComplete;

    // Player
    public string charName;
    public int playerLevel;
    public int currentEXP; //experience

    public int currentHP; //health points
    public int maxHP;

    public int currentMP; //magic points
    public int maxMP;
    public int strength;
    public int defence;
    public int wpnPwr; //weapon power
    public int armrPwr; //armour power
    public string equippedWpn;
    public string equippedArmr;

    public bool ifCharActive;


    public string currentScene;

    public string[] theItems;
    public int[] numberOfItem;
    public int currentMoney;

}