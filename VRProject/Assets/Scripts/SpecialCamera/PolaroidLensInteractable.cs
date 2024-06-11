using UnityEngine;

public class PolaroidLensInteractable : Interactable
{

    [SerializeField] private SpecialCamera specialCamera;
    [SerializeField] private Dialogue inspectionDialogue;

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("polaroid_lens_picked_up")) {
            specialCamera.lens = true;
            Destroy(gameObject);
        }
    }

    public override string GetLabel()
    {
        if (specialCamera.gameObject.activeSelf) 
            return InteractionLabels.EQUIP;
        else 
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (specialCamera.gameObject.activeSelf) {
            SaveSystem.SetFlag("polaroid_lens_picked_up");
            specialCamera.lens = true;
            Destroy(gameObject);
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }
}
