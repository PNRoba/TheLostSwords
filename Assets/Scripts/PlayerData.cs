using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position = new float[3];
    // public PlayerController player;

    public string[] questNames;
    public bool[] ifComplete;

// Player
    public string charName;
    public int playerLevel = 1;
    public int currentEXP; //experience

    public int currentHP; //health points
    public int maxHP = 100;

    public int currentMP; //magic points
    public int maxMP = 30;
    public int strength;
    public int defence;
    public int wpnPwr; //weapon power
    public int armrPwr; //armour power
    public string equippedWpn;
    public string equippedArmr;

    public bool ifCharActive;

// Player 1

    public string charName1;
    public int playerLevel1 = 1;
    public int currentEXP1; //experience

    public int currentHP1; //health points
    public int maxHP1 = 100;

    public int currentMP1; //magic points
    public int maxMP1 = 30;
    public int strength1;
    public int defence1;
    public int wpnPwr1; //weapon power
    public int armrPwr1; //armour power
    public string equippedWpn1;
    public string equippedArmr1;

    public bool ifCharActive1;

// Player 2

    public string charName2;
    public int playerLevel2 = 1;
    public int currentEXP2; //experience
    public int[] expToNextLevel2;

    public int currentHP2; //health points
    public int maxHP2 = 100;

    public int currentMP2; //magic points
    public int maxMP2 = 30;
    public int strength2;
    public int defence2;
    public int wpnPwr2; //weapon power
    public int armrPwr2; //armour power
    public string equippedWpn2;
    public string equippedArmr2;

    public bool ifCharActive2;

    public string currentScene;

    // public PlayerData(){
    //     player = PlayerController.instance;
    //     position = new float[3];
    //     position[0] = player.transform.position.x;
    //     position[1] = player.transform.position.y;
    //     position[2] = player.transform.position.z;
    // }

// Inventory

    public string[] theItems;
    public int[] numberOfItem;
    public int currentMoney;

}