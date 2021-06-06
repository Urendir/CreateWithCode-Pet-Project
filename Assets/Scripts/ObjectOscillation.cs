using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOscillation : MonoBehaviour
{
    
    [SerializeField][Tooltip("Provides the destination point for the object.")]
            Vector3 destinationVector;
    [SerializeField][Tooltip("Provides the duration for one back-and-forth journey. Increasing this will increase the speed of the object.")]
            [Range(0.2f, 60f)]float timeToFullyCycle = 6f; 
    [SerializeField][Tooltip("Provides an initial offset from the starting position of the movement. Intended to be used when multiple oscillating objects should move asyncroniously to each other")]
            [Range(0.0f, 60f)]float delayTimer = 0.0f; 

    Vector3 startingPosition;
    float movementFactor;   

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        if (timeToFullyCycle <= Mathf.Epsilon) { return; }

        float elapsedCycles = (delayTimer + Time.time) / timeToFullyCycle; // Count of cycles is continually growing over time.
        const float tau = Mathf.PI * 2;     // this is a constant of 3.1415*2, so 6.283....

        float rawSinWave = Mathf.Sin(elapsedCycles * tau);   //Mathf.Sin gives a value from -1 to 1. 
                                                            // Tau is one lap around the circle "that gives" the Sin-wave. 
                                                            //Tau times the cycles to determine where on the sinwave we are, somewhere between -1 and 1.

        movementFactor = (rawSinWave + 1) / 2;       //recalculation to have the rawSinWave go from 0 to 1 (So, +1 to go 0 to 2, then dividing by 2, so it goes 0 to 1). 
                                                    //This causes the startPos to be 0, so the oscillation starts there.

        Vector3 offsetFromStart = destinationVector * movementFactor;

        transform.position = startingPosition + offsetFromStart;
    }
}
