using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private GameObject _debugInventoryItem;
    [SerializeField] private GameObject _debugInventoryItem2;
    [SerializeField] private GameObject _selectedItemPreview;
    [SerializeField] private TextMeshProUGUI _noItemText;


    private static GridLayoutGroup s_slotsLayout;
    private static TextMeshProUGUI s_itemPreviewText;
    private static InventorySlot s_selectedInventorySlot;
    private static InventorySlot s_itemPreviewSlot;
    private static InventorySlot s_selectedCombineObject = null;
    private static Item s_itemPreview = null;
    private static readonly List<InventorySlot> s_itemSlots = new();
    
    public IList<Item> Items { get => s_itemSlots.Select(i => i.Item).AsReadOnlyList(); }


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

        bool noItems = s_itemSlots.Count() == 0;

        _selectedItemPreview.SetActive(!noItems);
        _noItemText.gameObject.SetActive(noItems);
        
    }

    private void Start()
    {
        s_slotsLayout = _inventoryCanvas.GetComponentInChildren<GridLayoutGroup>();        
        s_itemPreviewSlot = _selectedItemPreview.GetComponentInChildren<InventorySlot>();
        s_itemPreviewText = _selectedItemPreview.GetComponentInChildren<TextMeshProUGUI>();

        for(int i = 0; i < 3; ++i)
        {
            if(i % 2 == 0)
            {
                AddItem(_debugInventoryItem);
            } 
            else 
            {
                AddItem(_debugInventoryItem2);
            }
        }
    }

    private void Open()
    {
        _inventoryCanvas.gameObject.SetActive(true);
    }

    private void Close() 
    {
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
        
        Debug.Log(s_itemSlots.IndexOf(s_selectedInventorySlot));
    }

    public InventorySlot AddItem(GameObject gameObject)
    {
        GameObject itemObject = Instantiate(gameObject, Vector3.zero, Quaternion.Euler(45f, 0f, 45f));
        Item item = itemObject.GetComponent<Item>();

        GameObject slot = Instantiate(_inventorySlotPrefab);
        slot.transform.SetParent(s_slotsLayout.transform, false);

        InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
        slotComponent.GetComponent<Button>().onClick.AddListener(delegate {SetSelected(slotComponent);});
        slotComponent.GetComponentInChildren<ParticleSystem>().Stop();

        slotComponent.SetItem(item);
        s_itemSlots.Add(slotComponent);
        SetSelected(slotComponent);

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
            InventorySlot resultSlot = AddItem(result.gameObject);
            SetSelected(resultSlot);
            s_itemSlots.ForEach(slot => Debug.Log(slot.Selected));
        }
        return canCombine;
    }

    public void ExamineItem()
    {
        //GameObject item = Instantiate(s_selectedInventorySlot.Item);
        throw new NotImplementedException();
    }
}
