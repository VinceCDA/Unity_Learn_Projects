using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rpm;
    [SerializeField] private float horsePower = 0;
    [SerializeField] private float turnSpeed = 50.0f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody rb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedMeterText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Move forward
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        if (IsOnGround())
        {
            //Move forward
            rb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            //transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward);
            //Rotate based on horizontal input
            transform.Rotate(horizontalInput * Time.deltaTime * turnSpeed * Vector3.up);
            speed = rb.velocity.magnitude * 3.6f;
            speedMeterText.text = "Speed : " + Mathf.RoundToInt(speed) + " kmh";
            rpm = Mathf.Round(speed % 30) * 40;
            rpmText.text = "RPM : " + rpm;
        }
        
        
    }
    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        return false;
    }
}
