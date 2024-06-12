using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteractable : Interactable
{
    [SerializeField] private Dialogue inspectionDialogue;
    [SerializeField] private GameObject lockGo;
    [SerializeField] private GameObject key;
    [SerializeField] private Door door;

    private void Start() {
        if (Settings.load) {
            if (SaveSystem.CheckFlag("key_inserted"))
                key.SetActive(true);
            if (SaveSystem.CheckFlag("key_turned")) {
                lockGo.transform.Rotate(0, 0, 90);
                DisableInteraction();
            }
        }
    }

    public override string GetLabel()
    {
        if (key.activeSelf)
            return InteractionLabels.INTERACT;
        else if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.KEY))
            return InteractionLabels.USE_ITEM;
        else 
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (key.activeSelf) {
            TurnKey();
        }
        else if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.KEY)) {
            SaveSystem.SetFlag("key_inserted");
            PlayerInventory.Instance().ConsumeSelectedItem();
            key.SetActive(true);
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }

    private void TurnKey() {
        SaveSystem.SetFlag("key_turned");
        lockGo.transform.Rotate(0, 0, 90);
        door.Unlock();
        DisableInteraction();
    }
}
