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

    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private GameObject _selectedItemPreview;
    [SerializeField] private TextMeshProUGUI _noItemText;
    [SerializeField] private InspectionScreen inspectionScreen;

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !(Settings.paused || Settings.inventoryOn))
        {
            Open();
        }

        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && Settings.inventoryOn) 
        {
            StartCoroutine(Close());
        }

        bool noItems = s_itemSlots.Count() == 0;

        _selectedItemPreview.SetActive(!noItems);
        _noItemText.gameObject.SetActive(noItems);
        
    }

    private void Start()
    {
        s_slotsLayout = _inventoryCanvas.GetComponentInChildren<GridLayoutGroup>();        
        s_itemPreviewSlot = _selectedItemPreview.GetComponentInChildren<InventorySlot>();
        s_itemPreviewText = _selectedItemPreview.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    public void Open()
    {
        Settings.inventoryOn = true;
        CursorManager.ShowCursor();
        _inventoryCanvas.gameObject.SetActive(true);
    }

    //This is a coroutine so that pressing escape and going to the inventory happen on subsequent frames, otherwise
    //the inventory would also immediately close
    private IEnumerator Close() 
    {
        yield return null;
        Settings.inventoryOn = false;
        CursorManager.HideCursor();
        _inventoryCanvas.gameObject.SetActive(false);
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
        }
        
    }

    public InventorySlot AddItem(Item item)
    {
        _inventoryCanvas.gameObject.SetActive(true);
        GameObject slot = Instantiate(_inventorySlotPrefab);
        slot.transform.SetParent(s_slotsLayout.transform, false);

        InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
        slotComponent.GetComponent<Button>().onClick.AddListener(delegate {SetSelected(slotComponent);});
        slotComponent.GetComponentInChildren<ParticleSystem>().Stop();
        slotComponent.SetItem(item);

        s_itemSlots.Add(slotComponent);
        SetSelected(slotComponent);
        _inventoryCanvas.gameObject.SetActive(Settings.inventoryOn);
        return slotComponent;

    }

    private void RemoveItem(Item item)
    {
        InventorySlot slot = s_itemSlots.First(slot => slot.Item == item);
        s_itemSlots.Remove(slot);
        Destroy(slot.gameObject);
    }

    private void SetItemPreview()
    {
        s_itemPreview = Instantiate(s_selectedInventorySlot.Item, Vector3.zero, Quaternion.Euler(45f,0,45f));
        s_itemPreviewSlot.SetItem(s_itemPreview);
        s_itemPreviewText.SetText(s_itemPreview.Description);
        s_itemPreview.transform.localScale *= 1.8f;
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

    public void ExamineItem()
    {
        StartCoroutine(Close());
        inspectionScreen.StartInspecting(s_selectedInventorySlot.Item.gameObject);
        _inventoryCanvas.gameObject.SetActive(false);
    }
}
