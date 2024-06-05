using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory instance = null;

    private bool firstItem = true;

    [SerializeField] private Item[] itemPrefabs;

    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private GameObject _selectedItemPreview;
    [SerializeField] private GameObject _combineButton;
    // [SerializeField] private GameObject _inspectButton;
    [SerializeField] private TextMeshProUGUI _noItemText;
    // [SerializeField] private InspectionScreen inspectionScreen;

    private GridLayoutGroup s_slotsLayout;
    private TextMeshProUGUI s_itemPreviewText;
    private InventorySlot s_selectedInventorySlot;
    private InventorySlot s_itemPreviewSlot;
    private InventorySlot s_selectedCombineObject = null;
    private Item s_itemPreview = null;
    private readonly List<InventorySlot> s_itemSlots = new();
    
    public IList<Item> Items { get => s_itemSlots.Select(i => i.Item).AsReadOnlyList(); }

    public static PlayerInventory Instance() {
        if (instance == null)
            instance = FindObjectOfType<PlayerInventory>();
        return instance;
    }

    private void Start()
    {
        s_slotsLayout = _inventoryCanvas.GetComponentInChildren<GridLayoutGroup>();        
        s_itemPreviewSlot = _selectedItemPreview.GetComponentInChildren<InventorySlot>();
        s_itemPreviewText = _selectedItemPreview.GetComponentInChildren<TextMeshProUGUI>();

        Dictionary<string, Item> idToItem = new Dictionary<string, Item>();

        foreach (Item prefab in itemPrefabs) {
            idToItem[prefab.Id] = prefab;
        }

        if (Settings.load) {
            List<string> loadedItemsIds = SaveSystem.GetInventoryItems();
            string selectedItemId = SaveSystem.GetSelectedInventoryItem();
            
            foreach (string loadedItemId in loadedItemsIds) {
                AddItem(Instantiate(idToItem[loadedItemId]));
            }
            
            if (selectedItemId != null)
                SetSelected(s_itemSlots.First(slot => slot.Item.Id == selectedItemId));
            else 
                SetSelected(null);
        }    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !(Settings.paused || Settings.inventoryOn))
        {
            Open();
        }

        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && Settings.inventoryOn) 
        {
            Close();
        }

        bool noItems = s_itemSlots.Count() == 0;

        _selectedItemPreview.SetActive(!noItems);
        _combineButton.SetActive(!noItems);
        // _inspectButton.SetActive(!noItems);
        _noItemText.gameObject.SetActive(noItems);
        
    }

    public void Open()
    {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inventoryOn = true;
        CursorManager.ShowCursor();
        _inventoryCanvas.gameObject.SetActive(true);
    }

    private void Close() 
    {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inventoryOn = false;
        CursorManager.HideCursor();
        _inventoryCanvas.gameObject.SetActive(false);
        s_selectedCombineObject = null;
    }

    public void SetSelected(InventorySlot slot)
    {
        if(s_selectedCombineObject != null)
        {
            CombineItems(slot);
        } else {
            s_itemSlots.ForEach(other => other.Selected = other == slot);
            s_selectedInventorySlot = slot;
            SetItemPreview();

            SaveSystem.SetSelectedInventoryItem(slot == null ? null : s_selectedInventorySlot.Item.Id);
            Messenger<Item>.Broadcast(MessageEvents.SELECTED_ITEM, slot == null ? null : s_selectedInventorySlot.Item);
        }
        
    }

    public InventorySlot AddItem(Item item)
    {
        if (firstItem) {
            Messenger.Broadcast(MessageEvents.FIRST_ITEM_PICKED_UP);
            firstItem = false;
        }

        SaveSystem.AddInventoryItem(item);
        
        _inventoryCanvas.gameObject.SetActive(true);
        GameObject slot = Instantiate(_inventorySlotPrefab);
        slot.transform.SetParent(s_slotsLayout.transform, false);

        InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
        slotComponent.GetComponent<Button>().onClick.AddListener(delegate {SetSelected(slotComponent);});
        slotComponent.GetComponentInChildren<ParticleSystem>().Stop();
        slotComponent.SetItem(item, false);

        s_itemSlots.Add(slotComponent);
        SetSelected(slotComponent);
        _inventoryCanvas.gameObject.SetActive(Settings.inventoryOn);
        return slotComponent;

    }

    private void RemoveItem(Item item)
    {
        SaveSystem.RemoveInventoryItem(item);
        InventorySlot slot = s_itemSlots.First(slot => slot.Item == item);
        s_itemSlots.Remove(slot);
        Destroy(slot.gameObject);
    }

    private void SetItemPreview()
    {
        if (s_selectedInventorySlot == null) {
            s_itemPreviewSlot.ClearItem();
            s_itemPreviewText.SetText("");
        }
        else {
            s_itemPreview = Instantiate(s_selectedInventorySlot.Item);
            s_itemPreviewSlot.SetItem(s_itemPreview, true);
            s_itemPreviewText.SetText(s_itemPreview.Description);
        }
    }

    public void OnCombineClicked()
    {
        s_selectedCombineObject = s_selectedInventorySlot;
        s_selectedCombineObject.GetComponentInChildren<ParticleSystem>().Simulate(1f);
        s_selectedCombineObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    public bool CombineItems(InventorySlot slot)
    {
        Item firstItem = s_selectedCombineObject.Item;
        Item secondItem = slot.Item;

        s_selectedCombineObject.GetComponentInChildren<ParticleSystem>().Clear();
        s_selectedCombineObject.GetComponentInChildren<ParticleSystem>().Stop();
        s_selectedCombineObject = null;

        bool canCombine = ItemCombineRecipes.GetRecipeIfExists(firstItem.type, secondItem.type, out Item result);

        if(canCombine)
        {
            RemoveItem(firstItem);
            RemoveItem(secondItem);
            InventorySlot resultSlot = AddItem(Instantiate(result));
            SetSelected(resultSlot);
        }
        return canCombine;
    }

    // public void ExamineItem()
    // {
    //     StartCoroutine(ExamineItemCoroutine());
    // }

    // public IEnumerator ExamineItemCoroutine() {
    //     yield return Close();
    //     inspectionScreen.StartInspecting(s_selectedInventorySlot.Item.gameObject);
    //     _inventoryCanvas.gameObject.SetActive(false);
    // }

    //Checks if the selected item is of type type
    public bool CheckSelectedItem(Item.ItemType type) {
        if (s_selectedInventorySlot == null)
            return false;
        return s_selectedInventorySlot.Item.type == type;
    }

    public void ConsumeSelectedItem() {
        if (s_selectedInventorySlot != null) {
            RemoveItem(s_selectedInventorySlot.Item);
            SetSelected(null);            
        }
    }

}
