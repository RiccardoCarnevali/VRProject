using UnityEngine;

public class NailedPlanksInteractable : Interactable
{
    [SerializeField] private Dialogue inspectionDialogue;
    [SerializeField] GameObject[] nailedPlanks;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("nailed_planks_removed")) {
            foreach (GameObject nailedPlank in nailedPlanks)
                Destroy(nailedPlank);
            Destroy(GetComponent<Collider>());
        }
    }

    public override string GetLabel()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.CROWBAR)) {
            return InteractionLabels.USE_ITEM;
        }
        else {
            return InteractionLabels.INSPECT;
        }
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.CROWBAR)) {
            SaveSystem.SetFlag("nailed_planks_removed");
            foreach (GameObject nailedPlank in nailedPlanks)
                Destroy(nailedPlank);
            Destroy(GetComponent<Collider>());
            audioSource.Play();
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }
}
