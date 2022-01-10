using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWpn;
    public bool isArmour;
    [Header("Item Details")]
    public string itemName;
    public string desc;
    public int value;

    public Sprite itemSprite;
    [Header("Item Affect Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Weapon/Armour Details")]
    public int wpnStrength;
    public int armourStrength;
    
    // item menu, select item, use/equip button
    public void Use(int charToUseOn){ 
        CharacterStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if(isItem){
            if(affectHP){
                selectedChar.currentHP +=amountToChange;
                if(selectedChar.currentHP > selectedChar.maxHP){
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }
            if(affectMP){
                selectedChar.currentMP +=amountToChange;
                if(selectedChar.currentMP > selectedChar.maxMP){
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            } 
            if(affectStr){
                selectedChar.strength += amountToChange;
            }  
        }
        if(isWpn){
            if(selectedChar.equippedWpn != ""){
                if(GameManager.instance.CheckIfSpace(selectedChar.equippedWpn)){
                    GameManager.instance.AddItem(selectedChar.equippedWpn);
                }
            }

            selectedChar.equippedWpn = itemName;
            selectedChar.wpnPwr = wpnStrength;
            PlayerController.instance.canAttack = true;
        }
        if(isArmour){
            if(selectedChar.equippedArmr != ""){
                GameManager.instance.AddItem(selectedChar.equippedArmr);
            }

            selectedChar.equippedArmr = itemName;
            selectedChar.armrPwr = armourStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
