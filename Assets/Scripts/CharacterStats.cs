using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentEXP; //experience
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP; //health points
    public int maxHP = 100;
    public int[] mpLvlBonus;

    public int currentMP; //magic points
    public int maxMP = 30;
    public int strength;
    public int defence;
    public int wpnPwr; //weapon power
    public int armrPwr; //armour power
    public string equippedWpn;
    public string equippedArmr;
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i=2; i < expToNextLevel.Length; i++){
            //expToNextLevel[i] = expToNextLevel[i-1]+50;
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i-1]*1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.K)){
        //     AddExp(500);
        // }
    }

    public void AddExp(int expToAdd){
        if(playerLevel<maxLevel){
            currentEXP+=expToAdd;
            if(currentEXP > expToNextLevel[playerLevel]){
                currentEXP-= expToNextLevel[playerLevel];
                playerLevel++;

                //determine whether to add to str or def based on odd or even lvl
                if(playerLevel%2 == 0){
                    strength++;
                }else{
                    defence++;
                }

                maxHP = Mathf.FloorToInt(maxHP*1.05f);
                currentHP = maxHP;

                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }
        if(playerLevel>=maxLevel){
            currentEXP = 0;
        }
    }
}
