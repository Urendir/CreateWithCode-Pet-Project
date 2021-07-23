using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthAndShields : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;
    [SerializeField] int currentShield;
    [SerializeField] float shieldRegenDelay = 2f;
    [SerializeField] int shieldRegenAmount = 2;
    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] TextMeshProUGUI shieldDisplay;

    int maxShieldPoints = 50;    
    
    void Start()
    {
        currentShield = maxShieldPoints;
        StartCoroutine(IncreaseShields());
        UpdateShieldUI();
        UpdateHealthUI();
        
    }

    IEnumerator IncreaseShields()
    {
        while(true)
        {
            if(currentShield< maxShieldPoints)
            {
                currentShield += shieldRegenAmount;
                UpdateShieldUI();
                yield return new WaitForSeconds(shieldRegenDelay);
            }
            else
            {
                yield return null;
            }         
        }     
    }



    public void DecreaseHealth(int damageTaken)
    {
        int intermediateDamage;

        if(currentShield-damageTaken > 0)
        {
            currentShield -= damageTaken;
            UpdateShieldUI();
        } 
        else if(currentShield-damageTaken <0)
        {
            intermediateDamage = damageTaken-currentShield;
            currentShield = Mathf.Max(0, currentShield-damageTaken);
            healthPoints -= intermediateDamage;
            UpdateShieldUI();
            UpdateHealthUI();
        }

        if(healthPoints <= 0)
        {
            UpdateHealthUI();
            Destroy(gameObject);
        }
    }

    void UpdateShieldUI()
    {
        shieldDisplay.text = "Shield Power:" + currentShield;
    }

    void UpdateHealthUI()
    {
        healthDisplay.text = "Hull Integrity: " + healthPoints;
    }


    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetShields()
    {
        return currentShield;
    }
}
