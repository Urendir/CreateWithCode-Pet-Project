using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMineAttack : MonoBehaviour
{
    [SerializeField] int damagePotential = 60;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float attackSpeed = 2f;
    [SerializeField] float speedOfRotationOnAttack = 5f;
    [SerializeField][Tooltip("Provides the destination point for the object.")]
            Vector3 destinationVector;
    [SerializeField][Tooltip("Provides the duration for one back-and-forth journey. Increasing this will increase the speed of the object.")]
            [Range(0.2f, 60f)]float timeToFullyCycle = 6f; 
    [SerializeField][Tooltip("Provides an initial offset from the starting position of the movement. Intended to be used when multiple oscillating objects should move asyncroniously to each other")]
            [Range(0.0f, 60f)]float delayTimer = 0.0f; 

    Vector3 startingPosition;
    float movementFactor;  
    bool isAggravated = false; 

    GameObject player;
    Rigidbody myRb;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distancetoPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(distancetoPlayer < attackRange)
        {
            isAggravated = true;
            AttackBehaviour();
        }
        else
        {
            Oscillate();
        }
        
    }

    private void AttackBehaviour()
    {
        if(isAggravated)
        {
            attackRange = 100;
            transform.LookAt(Vector3.Lerp(transform.position, player.transform.position, speedOfRotationOnAttack));
            transform.position += transform.forward * attackSpeed * Time.deltaTime;
            GetComponent<Animator>().SetBool("isAggroed", true);
        }
    }

    private void Oscillate()
    {
        if (timeToFullyCycle <= Mathf.Epsilon) { return; }

        if(!isAggravated)
        {
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

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject == player)
        {
            player.GetComponent<Rigidbody>().AddForceAtPosition(myRb.velocity*20, transform.position, ForceMode.Force);
            player.GetComponent<HealthAndShields>().DecreaseHealth(damagePotential);
            Destroy(gameObject);
        }

    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    
}
