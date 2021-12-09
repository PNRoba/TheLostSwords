using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    
    public Text moneyText;

    public string[] itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDesc, buyItemValue;
    public Text sellItemName, sellItemDesc, sellItemValue;

    public Text itemMoneyDifference; // visually displays how much money was taken above money, red "-" /green "+"


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShop(){
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopOpen = true;

        moneyText.text = GameManager.instance.currentMoney.ToString() + "m";
    }

    public void CloseShop(){
        shopMenu.SetActive(false);

        GameManager.instance.shopOpen = false;
    }

    public void OpenBuyMenu(){
        buyItemButtons[0].Press();
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for(int i = 0; i<buyItemButtons.Length; i++){
            buyItemButtons[i].buttonValue = i;
            if(itemsForSale[i] != ""){
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }else{
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }
    public void OpenSellMenu(){
        sellItemButtons[0].Press();
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        showSellItems();
    }

    private void showSellItems(){
        GameManager.instance.ItemSorter();
        for(int i = 0; i<sellItemButtons.Length; i++){
            sellItemButtons[i].buttonValue = i;

            if(GameManager.instance.theItems[i] != ""){
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.theItems[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItem[i].ToString();
            }else{
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem){
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDesc.text = selectedItem.desc;
        buyItemValue.text = "Value: " + selectedItem.value  + "m";
        // itemMoneyDifference.text = "-" + selectedItem.value;
    }
    public void SelectSellItem(Item sellItem){
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDesc.text = selectedItem.desc;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "m";
        // itemMoneyDifference.text = "-" + selectedItem.value;
    }
    
    // public void difference(){}

    public void BuyItem(){
        if(selectedItem != null){
            if(GameManager.instance.currentMoney >=selectedItem.value){
            GameManager.instance.currentMoney -= selectedItem.value;

            GameManager.instance.AddItem(selectedItem.itemName);
            }
        }
        moneyText.text = GameManager.instance.currentMoney.ToString() + "m";
    }
    public void SellItem(){
        if(selectedItem != null){
            GameManager.instance.currentMoney += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);
        }
        moneyText.text = GameManager.instance.currentMoney.ToString() + "m";
        showSellItems();
    }
}
