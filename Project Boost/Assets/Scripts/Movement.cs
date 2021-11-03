using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;

    [SerializeField]
    float forwardThrust = 1000f;

    [SerializeField]
    float rotationThrust = 500f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            myRigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * forwardThrust);
        }
    }

    private void ProcessRotation() {
        myRigidbody.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime * rotationThrust);
        myRigidbody.freezeRotation = false;
    }
}
