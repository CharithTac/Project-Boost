using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    [SerializeField]
    float forwardThrust = 1000f;
    [SerializeField]
    float rotationThrust = 500f;
    [SerializeField]
    AudioClip thrustSound;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * forwardThrust);
            if (!mainThrusterParticles.isEmitting) {
                mainThrusterParticles.Play();
            }

            if (!myAudioSource.isPlaying) 
            { 
                myAudioSource.PlayOneShot(thrustSound);
            }
        }
        else {
            myAudioSource.Stop();
            mainThrusterParticles.Stop();
        }
    }

    private void ProcessRotation() {
        myRigidbody.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime * rotationThrust);
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (!leftThrusterParticles.isEmitting)
            {
                leftThrusterParticles.Play();
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (!rightThrusterParticles.isEmitting)
            {
                rightThrusterParticles.Play();
            }
        }
        else {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }

        myRigidbody.freezeRotation = false;
    }
}
