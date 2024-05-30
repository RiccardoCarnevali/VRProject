using UnityEngine;

public class PolaroidInteractable : Interactable
{
    [SerializeField] private GameObject polaroid;
    
    public override string GetLabel()
    {
        return "Pick up";
    }

    public override void Interact()
    {
        polaroid.SetActive(true);
        Destroy(gameObject);
        Messenger.Broadcast(MessageEvents.POLAROID_PICKED_UP);
    }
}
