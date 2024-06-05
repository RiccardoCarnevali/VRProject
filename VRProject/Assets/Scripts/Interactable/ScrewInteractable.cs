using UnityEngine;

public class ScrewInteractable : Interactable
{

    [SerializeField] private int id;
    [SerializeField] private Dialogue inspectionDialgoue;

    [SerializeField] private ScrewedBox screwedBox;

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("screw_" + id + "_removed")) {
            Destroy(gameObject);
        }
    }

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
            SaveSystem.SetFlag("screw_" + id + "_removed");
            Destroy(gameObject);
            screwedBox.ScrewRemoved();
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialgoue);
        }
    }
}
