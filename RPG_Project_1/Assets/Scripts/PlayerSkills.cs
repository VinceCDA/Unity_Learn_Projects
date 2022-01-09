using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField]
    public GameObject UIpanel;
    public Text pointsText;

    public int pointsDisponible;
    public string openKey;

    private bool isOpen;
    private PlayerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            isOpen = !isOpen;
        }
        if (isOpen)
        {
            pointsText.text = "Points Disponibles : " + pointsDisponible;
            UIpanel.SetActive(true);
        }
        else
        {
            UIpanel.SetActive(false);
        }
    }
    public void AddHealthMax(float amountHP)
    {
        if (pointsDisponible >= 1)
        {
            inventory.maxHealth += amountHP;
            inventory.currentHealth += amountHP;
            pointsDisponible --;
        }
    }
    public void AddManaMax(float amountMana)
    {
        if (pointsDisponible >= 1)
        {
            inventory.maxMana += amountMana;
            inventory.currentMana += amountMana;
            pointsDisponible --;
        }
    }
}
