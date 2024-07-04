using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IInteractable
{
    public static event Action<ItemData> OnAmmoPickupCollected;

    public ItemData ItemData;

    public void Interact()
    {
        Destroy(gameObject);
        OnAmmoPickupCollected?.Invoke(ItemData);
    }
}
