using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : Interactable
{
    public override string GetLabel()
    {
        return "Pick up";
    }

    public override void Interact()
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();
        inventory.AddItem(GetComponent<Item>());
 
        Debug.Log("Picked up");
    }
}
