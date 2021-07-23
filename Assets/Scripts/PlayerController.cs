using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float forwardThrust = 1200f;
    [SerializeField] float backwardThrust = 150f;
    [SerializeField] float lateralThrust = 300f;

    Rigidbody myRigidBody;
    EnergyReserves playerEnergy;

    bool isRotating = false;
    float collisionVelocityMagnitude = 0;

    bool forwardThrustActive = false;
    bool reverseThrustActive = false;
    bool navigationalThrustActive = false;

    private void Start() 
    {
        myRigidBody = GetComponent<Rigidbody>();
        playerEnergy = GetComponent<EnergyReserves>();
        // StartCoroutine(ConsumeEnergyMainThruster());
        // StartCoroutine(ConsumeEnergySmallThruster());
    }

    private void Update()
    {
        float energy = playerEnergy.GetCurrentEnergy();

        if(energy > 0)
        {
            ForwardMovement();
            BackwardMovement();
            ProcessRotation();
        }
    }

    private void ForwardMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forwardThrustActive = true;
            myRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * forwardThrust, ForceMode.Force);
            playerEnergy.MainThrusterFuelConsumption();
        }
        else
        {
            forwardThrustActive = false;
        }
    }

    private void BackwardMovement()
    {
        if (Input.GetKey(KeyCode.S))
        {
            reverseThrustActive = true;
            myRigidBody.AddRelativeForce(Vector3.down * Time.deltaTime * backwardThrust, ForceMode.Force);
            playerEnergy.NavigationalThrusterConsumption();
        }
        else
        {
            reverseThrustActive = false;
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            isRotating = true;
            navigationalThrustActive = true;
            LateralMovement(lateralThrust);
        } 
        else if(Input.GetKey(KeyCode.D))
        {
            isRotating = true;
            navigationalThrustActive = true;
            LateralMovement(-lateralThrust);
        }
        else
        {
            isRotating = false;
            navigationalThrustActive = false;
        }
    }

    private void LateralMovement(float rotationMultiplier)
    {
        if(isRotating)
        {
            myRigidBody.freezeRotation = true;
            playerEnergy.NavigationalThrusterConsumption();
            transform.Rotate(Vector3.forward * rotationMultiplier * Time.deltaTime);
            myRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    IEnumerator ConsumeEnergySmallThruster()
    {
        if(navigationalThrustActive || reverseThrustActive)
        {
           playerEnergy.NavigationalThrusterConsumption();
           yield return new WaitForSeconds(1); 
        } 
    }

    IEnumerator ConsumeEnergyMainThruster()
    {
        if(forwardThrustActive)
        {
           playerEnergy.MainThrusterFuelConsumption();
           yield return new WaitForSeconds(1); 
        } 
    }

    private void OnCollisionEnter(Collision other) 
    {
        collisionVelocityMagnitude = myRigidBody.velocity.magnitude;
        int collisionDamage = (int) collisionVelocityMagnitude * 3;
        GetComponent<HealthAndShields>().DecreaseHealth(collisionDamage);
        Debug.Log("Damage taken from collision:" + collisionDamage);
    }
}
