using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Sprite _normalBg, _selectedBg;

    private bool _selected;

    public Item Item { get; private set; }
    public PlayerInventory Inventory;
    [property: SerializeField] public bool Selected {
        get {return _selected;} 
        set {
            if(_selected != value)
            {
                _selected = value;
                GetComponent<Image>().sprite = _selected ? _selectedBg : _normalBg;
            }
        }
    }


    public void Awake()
    {
        _selected = false;
    }

    public void ClearItem()
    {
        if(Item != null)
        {
            Debug.Log("Test");
            Destroy(Item.gameObject);
        }
            
    }

    public void SetItem(Item item)
    {
        ClearItem();
        Item = item;
        Item.transform.SetParent(transform, false);
        Item.transform.localPosition = new(0,0,-100);

        ResizeItemToFitTile();
        Item.gameObject.layer = LayerMask.NameToLayer(Settings.UI_LAYER);

    }

    private void ResizeItemToFitTile()
    {
        if(!Item.TryGetComponent<Renderer>(out Renderer renderer))
            return;
        Bounds bound = renderer.bounds;
        float maxBound = Mathf.Abs(Mathf.Max(bound.size.x, bound.size.y, bound.size.z));
        Item.transform.localScale = new (
            Item.transform.localScale.x / maxBound, 
            Item.transform.localScale.y / maxBound, 
            Item.transform.localScale.z / maxBound
            );

        
    }
}
