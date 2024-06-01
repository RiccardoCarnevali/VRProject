using UnityEngine;

public class ScrewInteractable : Interactable
{

    [SerializeField] private Dialogue inspectionDialgoue;

    [SerializeField] private ScrewedBox screwedBox;

    public override string GetLabel()
    {
        if (IsScrewdriverSelected())
            return "Use item";
        else
            return "Inspect";
    }

    public override void Interact()
    {
        if (IsScrewdriverSelected()) {
            Destroy(gameObject);
            screwedBox.ScrewRemoved();
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialgoue);
        }
    }

    private bool IsScrewdriverSelected() {
        Item item = PlayerInventory.Instance().GetSelectedItem();
        return item != null && item.type == Item.ItemType.SCREWDRIVER;
    }
}
