using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInnerActivator : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Portal Activated. Victory!");
    }
}
