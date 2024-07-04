using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData ItemData;
    public int StackSize;

    public InventoryItem(ItemData item)
    {
        ItemData = item;
        AddToStack();
    }

    public void AddToStack()
    {
        StackSize++;
    }
    public void RemoveFromStack()
    {
        StackSize--;
    }
}
