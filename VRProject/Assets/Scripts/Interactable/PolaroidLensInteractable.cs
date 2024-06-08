using UnityEngine;

public class PolaroidLensInteractable : Interactable
{

    [SerializeField] private SpecialCamera specialCamera;
    [SerializeField] private Dialogue inspectionDialogue;

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("polaroid_lens_picked_up")) {
            specialCamera.hasLens = true;
            Destroy(gameObject);
        }
    }

    public override string GetLabel()
    {
        if (specialCamera.gameObject.activeSelf) 
            return InteractionLabels.PICK_UP;
        else 
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (specialCamera.gameObject.activeSelf) {
            SaveSystem.SetFlag("polaroid_lens_picked_up");
            specialCamera.hasLens = true;
            Destroy(gameObject);
            Messenger.Broadcast(MessageEvents.POLAROID_LENS_PICKED_UP);
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }
}
