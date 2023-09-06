using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f); // Move it 10 in each direction
    [SerializeField] float period = 2f; // 2 seconds

    float movementFactor; // 0 for not moved, 1 for fully moved

    Vector3 startingPos;

    void Start() {
        startingPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate() {
        PlatformMovement();
    }

    private void PlatformMovement() {
        if (period <= Mathf.Epsilon) { return; } // protects against period is zero
        float cycles = Time.time / period; // Time.time refers to game time grows cycles continously

        const float tau = Mathf.PI * 2; //tau is 2 pie
        float rawSinWave = Mathf.Sin(cycles * tau); // raw sin wave is the up down sine wave
        //print(rawSinWave); // goes between 1 and -1 because of the cycles * tau

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
