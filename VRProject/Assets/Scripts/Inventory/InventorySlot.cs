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
    [property: SerializeField]
    public bool Selected
    {
        get { return _selected; }
        set
        {
            if (_selected != value)
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
        if (Item != null)
        {
            Destroy(Item.gameObject);
        }

    }

    public void SetItem(Item item, bool preview)
    {
        ClearItem();
        Item = item;
        Item.transform.SetParent(transform, false);

        //In preview the object is already scaled to the canvas so we just make it a bit bigger
        if (preview) {
            Item.transform.SetLocalPositionAndRotation(new(item.Offset.x*2, item.Offset.y*2, -100), Quaternion.Euler(Item.Rotation));
            Item.transform.localScale = Item.transform.localScale * 2;
        }
        else {
            Item.transform.SetLocalPositionAndRotation(new(item.Offset.x, item.Offset.y, -100), Quaternion.Euler(Item.Rotation));
            Item.transform.localScale = Vector3.one * Item.Scale;
        }
        
    }
}
