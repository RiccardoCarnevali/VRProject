using UnityEngine;

public class DisplayInteractable : Interactable
{
    [SerializeField] private Dialogue noRemoteInspectionDialogue;
    [SerializeField] private Dialogue remoteInspectionDialogue;

    private AudioSource audioSource;
    [SerializeField] private GameObject solutionDigit;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("display_turned_on")) {
            DisableInteraction();
            solutionDigit.SetActive(true);
        }
    }

    public override string GetLabel()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.REMOTE) || PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.CHARGED_REMOTE)) {
            return InteractionLabels.USE_ITEM;
        }
        else {
            return InteractionLabels.INSPECT;
        }
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.REMOTE)) {
            DialogueManager.Instance().StartDialogue(remoteInspectionDialogue);
        }
        else if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.CHARGED_REMOTE)) {
            SaveSystem.SetFlag("display_turned_on");
            audioSource.Play();
            solutionDigit.SetActive(true);
            DisableInteraction();
        }
        else {
            DialogueManager.Instance().StartDialogue(noRemoteInspectionDialogue);
        }
    }
}
