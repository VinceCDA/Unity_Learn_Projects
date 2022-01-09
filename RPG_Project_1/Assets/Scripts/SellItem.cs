using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    CharacterMotor characterMotor;
    PlayerInventory playerInventory;
    Tooltip tooltip;
    // Start is called before the first frame update
    void Start()
    {
        characterMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SellItemOnShop()
    {
        if (Input.GetKey(KeyCode.LeftShift) && characterMotor.isInShop)
        {
            //List<Item> item = playerInventory.inventory.GetComponent<Inventory>().getItemList();
            playerInventory.goldCoins += gameObject.GetComponent<ItemOnObject>().item.itemValue;
            tooltip.deactivateTooltip();
            Destroy(gameObject);
            
        }
    }
}
