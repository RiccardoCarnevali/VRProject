using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{

    public enum ItemType
    {
        [InspectorName("Screwdriver")] SCREWDRIVER,
    }

    [field: SerializeField] public string Description {get; private set;}
    [Searchable] public ItemType type;

    void Start()
    {
        
    }
    
}

