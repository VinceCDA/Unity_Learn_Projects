using System.Collections.Generic;
using UnityEngine;

public class CheckWeapon : MonoBehaviour
{
    //Id de l'arme actuelle
    private int idArme;

    //Membre du personnage
    public GameObject bodyPart;

    //Liste d'armes
    public List<GameObject> listeArmes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            idArme = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else
        {
            idArme = 0;
            for (int i = 0; i < listeArmes.Count; i++)
            {

                listeArmes[i].SetActive(false);

            }
        }

        if (bodyPart.transform.childCount > 1)
        {
            for (int i = 0; i < listeArmes.Count; i++)
            {
                listeArmes[i].SetActive(false);
            }
        }
        if (idArme == 1 && transform.childCount > 0)
        {
            for (int i = 0; i < listeArmes.Count; i++)
            {
                if (i == 0)
                {
                    listeArmes[i].SetActive(true);
                }

            }
        }
    }
}
