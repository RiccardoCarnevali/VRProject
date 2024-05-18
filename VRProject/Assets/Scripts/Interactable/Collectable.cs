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
        PlayerInventory.Instance().AddItem(GetComponent<Item>());
    }
}
