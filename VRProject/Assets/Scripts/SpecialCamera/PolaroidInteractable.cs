using UnityEngine;

public class PolaroidInteractable : Interactable
{
    [SerializeField] private GameObject polaroid;

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("polaroid_picked_up")) {
            polaroid.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    public override string GetLabel()
    {
        return InteractionLabels.EQUIP;
    }

    public override void Interact()
    {
        SaveSystem.SetFlag("polaroid_picked_up");
        polaroid.SetActive(true);
        Destroy(gameObject);
        Messenger.Broadcast(MessageEvents.POLAROID_PICKED_UP);
    }
}
