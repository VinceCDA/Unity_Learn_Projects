using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    CharacterMotor characterMotor;
    public Inventory inventory;
    public PlayerInventory playerInventory;
    public GameObject shopPanel;
    public ItemDataBaseList itemDataBase;
    [Header("ID des items du shop")]
    Item item1;
    Item item2;
    Item item3;

    public int item1Id;
    public int item2Id;
    public int item3Id;

    public Text item1Text;
    public Text item2Text;
    public Text item3Text;

    public Image iconItem1;
    public Image iconItem2;
    public Image iconItem3;

    public int amountSlots;
    public int freeSlots;
    private bool transactionDone;
    // Start is called before the first frame update
    void Start()
    {
        characterMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PrepareShop()
    {
        item1 = itemDataBase.getItemByID(item1Id);
        item2 = itemDataBase.getItemByID(item2Id);
        item3 = itemDataBase.getItemByID(item3Id);

        item1Text.text = item1.itemName + " (Prix : " + item1.itemValue + ") ";
        item2Text.text = item2.itemName + " (Prix : " + item2.itemValue + ") ";
        item3Text.text = item3.itemName + " (Prix : " + item3.itemValue + ") ";

        iconItem1.sprite = item1.itemIcon;
        iconItem2.sprite = item2.itemIcon;
        iconItem3.sprite = item3.itemIcon;

        iconItem1.transform.GetComponent<Button>().onClick.AddListener(() => BuyItem(item1));
        iconItem2.transform.GetComponent<Button>().onClick.AddListener(() => BuyItem(item2));
        iconItem3.transform.GetComponent<Button>().onClick.AddListener(() => BuyItem(item3));

        shopPanel.SetActive(true);
    }
    void BuyItem(Item buyItem)
    {
        amountSlots = inventory.transform.GetChild(1).childCount;
        transactionDone = false;
        freeSlots = 0;

        foreach (Transform child in inventory.transform.GetChild(1))
        {
            if (child.childCount == 0)
            {
                freeSlots++;
                if (playerInventory.goldCoins >= buyItem.itemValue)
                {
                    inventory.addItemToInventory(buyItem.itemID);
                    playerInventory.goldCoins -= buyItem.itemValue;
                    transactionDone = true;
                    Debug.Log("Le joueur a acheté l'objet : " + buyItem.itemName);
                    break;
                }
                else
                {
                    Debug.Log("Pas assez d'argent");
                    break;
                }
                
            }
            
        }
        if (freeSlots == 0 && transactionDone == false)
        {
            Debug.Log("Inventaire plein.");
        }
    }
    void ExitShop()
    {
        iconItem1.GetComponent<Button>().onClick.RemoveAllListeners();
        iconItem2.GetComponent<Button>().onClick.RemoveAllListeners();
        iconItem3.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            characterMotor.isInShop = true;
            PrepareShop();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ExitShop();
            characterMotor.isInShop = false;
            shopPanel.SetActive(false);
        }
    }
}
