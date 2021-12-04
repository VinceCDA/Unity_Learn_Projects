using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    
    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        Material material = Renderer.material;
        
        material.color = new Color(Random.value, Random.value, Random.value); ;
    }
    
    void Update()
    {
        transform.Rotate(100.0f * Time.deltaTime, 0.0f, 0.0f);
        transform.Translate(Vector3.forward * Time.deltaTime * 10);
        Renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
