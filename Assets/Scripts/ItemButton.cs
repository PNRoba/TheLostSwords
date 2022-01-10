using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    // If selected item in the inventory or Shop
    // Gets and displays item details
    public void Press(){
        if(GameMenu.instance.theMenu.activeInHierarchy){
            if(GameManager.instance.theItems[buttonValue] != ""){
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.theItems[buttonValue]));
            }
        }
        if(Shop.instance.shopMenu.activeInHierarchy){
            if(Shop.instance.buyMenu.activeInHierarchy){
                Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
            } 
            if(Shop.instance.sellMenu.activeInHierarchy){
                Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.theItems[buttonValue]));
            }  
        }
    }
}
