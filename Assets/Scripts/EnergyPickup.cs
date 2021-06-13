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
        //playerEnergy = other.gameObject.GetComponent<EnergyReserves>();
        playerEnergy = FindObjectOfType<EnergyReserves>();

        if(playerEnergy !=null)
        {
            playerEnergy.AddEnergy(energyToAdd);
            Debug.Log("Adding Energy to Energy System:" + energyToAdd);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("EnergyReserves not found. Destroying Object");
            Destroy(gameObject);
        }
    }

}
