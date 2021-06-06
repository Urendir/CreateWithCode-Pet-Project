using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyReserves : MonoBehaviour
{
    [SerializeField] float maxEnergy = 100;
    [SerializeField] float currentEnergy;



    private void Start() 
    {
        currentEnergy = maxEnergy;
    }
    
    public void AddEnergy(int energyToAdd)
    {
        currentEnergy = Mathf.Max(currentEnergy+energyToAdd, maxEnergy);
    }

    public void MainThrusterFuelConsumption()
    {
        currentEnergy -= 3 * Time.deltaTime;
    }

    public void NavigationalThrusterConsumption()
    {
        currentEnergy -= 1 * Time.deltaTime;
    }


    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

}
