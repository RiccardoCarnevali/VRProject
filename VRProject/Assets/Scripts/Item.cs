using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{

    public enum ItemType
    {
        [InspectorName("Cube")] CUBE = 0,
        [InspectorName("Cylinder")] CYLINDER = 1,
        [InspectorName("Sphere")] SPHERE = 2
    }

    [field: SerializeField] public string Description {get; private set;}
    [Searchable] public ItemType type;

    void Start()
    {
        
    }
    
}

