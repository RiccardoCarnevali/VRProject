using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject Item { get; private set; } = null;
    public Image Image { get; private set; } = null;
    void Start()
    {
        
    }

    public void SetItem(GameObject item)
    {
        Item = item;
        //add item as image
        throw new NotImplementedException();
    }
}
