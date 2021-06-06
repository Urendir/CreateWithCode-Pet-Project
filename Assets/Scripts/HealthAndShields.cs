using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndShields : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;
    int maxShieldPoints = 50;
    [SerializeField] int currentShield;
    [SerializeField] float shieldRegenDelay = 2f;
    [SerializeField] int shieldRegenAmount = 2;

    
    
    void Start()
    {
        currentShield = maxShieldPoints;
        StartCoroutine(IncreaseShields());
    }

    IEnumerator IncreaseShields()
    {
        while(true)
        {
            if(currentShield< maxShieldPoints)
            {
                currentShield += shieldRegenAmount;
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
        } 
        else if(currentShield-damageTaken <0)
        {
            intermediateDamage = damageTaken-currentShield;
            currentShield = Mathf.Max(0, currentShield-damageTaken);
            Debug.Log("Current Shields: " + currentShield + " Current Health:" + healthPoints);
            Debug.Log("Substracting Damage from health: " + intermediateDamage);
            healthPoints -= intermediateDamage;
            Debug.Log("Current Shields: " + currentShield + " Current Health:" + healthPoints);
        }

        if(healthPoints <= 0)
        {
            Debug.Log("Current Shields: " + currentShield + " Current Health:" + healthPoints);
            Debug.Log("Game Over.");
            Destroy(gameObject);
        }
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
