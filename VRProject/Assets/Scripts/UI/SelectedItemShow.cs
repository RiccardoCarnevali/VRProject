using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemShow : MonoBehaviour
{
    [SerializeField] private GameObject selectedItemSpot;
    private GameObject selectedItem;

    void Start()
    {
        Messenger<Item>.AddListener(MessageEvents.SELECTED_ITEM, OnSelectedItem);
    }

    private void OnDestroy() {
        Messenger<Item>.RemoveListener(MessageEvents.SELECTED_ITEM, OnSelectedItem);
    }

    private void OnSelectedItem(Item item) {

        Destroy(selectedItem);

        if (item == null)
            return;

        Item itemCopy = Instantiate(item, selectedItemSpot.transform);
        itemCopy.transform.SetLocalPositionAndRotation(Vector3.zero,  Quaternion.Euler(item.Rotation));
        selectedItem = itemCopy.gameObject;
        itemCopy.transform.localScale = Vector3.one * 3 * itemCopy.Scale;
    }
}
