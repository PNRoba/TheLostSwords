using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Picking up script for items
public class PickUpItem : MonoBehaviour
{
    private bool canPickUp;

    // Update is called once per frame
    void Update()
    {
        if(canPickUp){ //  && Input.GetButtonDown("Fire1")
            if(GameManager.instance.CheckIfSpace(GetComponent<Item>().itemName)){
                GameManager.instance.AddItem(GetComponent<Item>().itemName);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            canPickUp = false;
        }
    }
}