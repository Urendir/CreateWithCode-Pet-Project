using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [SerializeField] float energyToAdd = 25;
    [SerializeField] Vector3 rotationAngle;
    EnergyReserves playerEnergy;


    private void Update() 
    {
        transform.Rotate(rotationAngle * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other) 
    {
        playerEnergy = other.gameObject.GetComponent<EnergyReserves>();

        if(playerEnergy !=null)
        {
            playerEnergy.AddEnergy(energyToAdd);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
