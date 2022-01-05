using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public bool canOpen;

    public string[] itemsForSale = new string[40];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(canOpen && Input.GetKeyDown(KeyCode.E) && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy){
        //     Shop.instance.itemsForSale = itemsForSale;
            
        //     Shop.instance.OpenShop();
        // }

        if(canOpen && Input.GetButtonDown("Fire1")){ // Input.GetKeyDown(KeyCode.E)
            if(Shop.instance.shopMenu.activeInHierarchy){
                //Shop.instance.CloseShop();
            }else{
                Debug.Log(itemsForSale[0]);
                Shop.instance.itemsForSale = itemsForSale;
                Debug.Log(Shop.instance.itemsForSale[0]);
                Shop.instance.OpenShop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            canOpen = false;
        }
    }
}
