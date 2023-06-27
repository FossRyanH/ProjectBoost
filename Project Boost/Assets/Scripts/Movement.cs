using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 25f;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    Rigidbody rocketRB;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(thrustSpeed);
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ProcessRotation(float rotationThisFrame)
    {
        // Freeze rotation for manual rotation
        rocketRB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRB.freezeRotation = false;
    }

    void ProcessThrust(float moveSpeed)
    {
        // If the spacebar is pressed, play sounds and move the rocket
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust(moveSpeed);
        }
        else
        {
            StopThrust();
        }
    }


    void Thrust(float moveSpeed)
    {
        rocketRB.AddRelativeForce(Vector3.up * moveSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSFX, 1f);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    void RotateRight()
    {
        // Rotate to the Right
        ProcessRotation(-rotationSpeed);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }

    void RotateLeft()
    {
        // Rotates the ship to the left
        ProcessRotation(rotationSpeed);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }

    void StopRotating()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }
}
