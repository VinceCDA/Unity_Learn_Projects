using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh Text;
    public ParticleSystem SparksParticles;

    private List<string> TextToDisplay = new List<string>();

    private float RotatingSpeed;
    private float TimeToNextText;

    private int CurrentText;

    // Start is called before the first frame update
    void Start()
    {
        SparksParticles = GameObject.Find("SparksEffect").GetComponent<ParticleSystem>();
        TimeToNextText = 0.0f;
        CurrentText = 0;


        RotatingSpeed = 1.0f;

        TextToDisplay.Add("Congratulation");
        TextToDisplay.Add("All Errors Fixed");

        Text.text = TextToDisplay[0];

        SparksParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        TimeToNextText += Time.deltaTime;
        Text.transform.RotateAround(Vector3.zero, Vector3.up, RotatingSpeed);
        //Text.transform.Rotate(transform.up, RotatingSpeed);

        if (TimeToNextText > 1.5f)
        {
            TimeToNextText = 0.0f;

            CurrentText++;
            
            if (CurrentText >= TextToDisplay.Count)
            {
                CurrentText = 0;

            }

            Text.text = TextToDisplay[CurrentText];
        }
    } 
}