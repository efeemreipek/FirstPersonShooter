using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted " + name);
    }
}
