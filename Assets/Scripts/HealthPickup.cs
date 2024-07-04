using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    public static event Action<ItemData> OnHealthPickupCollected;

    public ItemData ItemData;

    public void Interact()
    {
        Destroy(gameObject);
        OnHealthPickupCollected?.Invoke(ItemData);
    }
}
