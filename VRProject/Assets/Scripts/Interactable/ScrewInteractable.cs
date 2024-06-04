using UnityEngine;

public class ScrewInteractable : Interactable
{

    [SerializeField] private Dialogue inspectionDialgoue;

    [SerializeField] private ScrewedBox screwedBox;

    public override string GetLabel()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.SCREWDRIVER))
            return InteractionLabels.USE_ITEM;
        else
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.SCREWDRIVER)) {
            Destroy(gameObject);
            screwedBox.ScrewRemoved();
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialgoue);
        }
    }
}
