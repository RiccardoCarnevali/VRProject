using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventorySlotPrefab;

    private static InventorySlot s_selectedInventorySlot;

    private readonly List<InventorySlot> _itemSlots = new();
    private readonly static int s_itemSlots = 30;
    public IList<GameObject> Items { get => _itemSlots.Select(i => i.Item).AsReadOnlyList(); }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Open();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Close();
        }
    }

    private void Start()
    {
        GridLayout slotsLayout = _inventoryCanvas.GetComponentInChildren<GridLayout>();
        for(int i = 0; i < s_itemSlots; i++)
        {
            GameObject slot = Instantiate(_inventorySlotPrefab);
            slot.transform.parent = slotsLayout.transform;
            _itemSlots.Add(slot.GetComponent<InventorySlot>());
        }
        s_selectedInventorySlot = _itemSlots.First();
    }

    private void Open()
    {
        _inventoryCanvas.gameObject.SetActive(true);
    }

    private void Close() 
    {
        _inventoryCanvas.gameObject.SetActive(false);
    }

    public void AddItem(GameObject item)
    {
        InventorySlot slot = _itemSlots.First(slot => slot.Item != null);
        slot.Item = item;
    }

    private void RemoveItem(GameObject item)
    {
        throw new NotImplementedException();
    }

    public bool CombineItems()
    {
        throw new NotImplementedException();
    }

    public void ExamineItem(InventorySlot slot)
    {

        slot.SetSelected(true);
        _inventoryCanvas.transform.Find(Settings.INVENTORY_SELECTED_ITEM_PREVIEW_IMAGE).GetComponent<Image>() = ;
    }
}
