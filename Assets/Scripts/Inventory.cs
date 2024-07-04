using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> InventoryList = new List<InventoryItem>();

    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        HealthPickup.OnHealthPickupCollected += Add;
        AmmoPickup.OnAmmoPickupCollected += Add;
    }

    private void OnDisable()
    {
        HealthPickup.OnHealthPickupCollected -= Add;
        AmmoPickup.OnAmmoPickupCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.ItemData.DisplayName} total stack is now {item.StackSize}.");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            InventoryList.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.DisplayName} to the inventory for the first time.");
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.StackSize == 0)
            {
                InventoryList.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }
}
