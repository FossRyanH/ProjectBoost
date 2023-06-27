using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    Vector3 startingPos;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Continually grows over time.
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        // cycles from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        // Recalulated from 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        // Moves the obstacle itself.
        transform.position = startingPos + offset;
    }
}
