using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortCollision : MonoBehaviour
{
    [SerializeField]
    public float sortDegat;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().AppliquerDegat(sortDegat);
            
        }
        Destroy(gameObject);
    }
}
