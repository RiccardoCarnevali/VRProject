using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] private Item item;

    private void Start() {

        if (Settings.load && SaveSystem.CheckFlag(item.Id + "_picked_up")) {
            Destroy(gameObject);
        }
    }

    public override string GetLabel()
    {
        return InteractionLabels.PICK_UP;
    }

    public override void Interact()
    {
        SaveSystem.SetFlag(item.Id + "_picked_up");
        Destroy(gameObject);
        PlayerInventory.Instance().AddItem(Instantiate(item));
    }
}
