using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyReserves : MonoBehaviour
{
    [SerializeField] float maxEnergy = 100;
    [SerializeField] float currentEnergy;
    [SerializeField] TextMeshProUGUI energyUIText;



    private void Start() 
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }
    
    public void AddEnergy(float energyToAdd)
    {
        
        float energyAdditive = currentEnergy + energyToAdd;
        currentEnergy = Mathf.Min(energyAdditive, maxEnergy);
        UpdateEnergyUI();
    }

    public void MainThrusterFuelConsumption()
    {
        currentEnergy -= 3 * Time.deltaTime;
        UpdateEnergyUI();
    }

    public void NavigationalThrusterConsumption()
    {
        currentEnergy -= 1 * Time.deltaTime;
        UpdateEnergyUI();       
    }

    void UpdateEnergyUI()
    {
        energyUIText.text = "Energy: "+ currentEnergy;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

}
