using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    public override string GetLabel()
    {
        return "Pick up";
    }

    public override void Interact()
    {
        Destroy(gameObject);
        Debug.Log("Picked up");
    }
}
