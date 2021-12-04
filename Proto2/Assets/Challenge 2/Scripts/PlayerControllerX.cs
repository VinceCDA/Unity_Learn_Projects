using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float coolDown = 30.0f;
    public float timer;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > timer)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timer = Time.time + coolDown;
        }
    }
}
