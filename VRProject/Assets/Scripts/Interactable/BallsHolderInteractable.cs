using UnityEngine;

public class BallsHolderInteractable : Interactable
{
    private int inserted = 0;
    [SerializeField] private GameObject[] balls;

    [SerializeField] private Dialogue inspectionDialogue;
    
    [SerializeField] private Door door;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        if (Settings.load) {
            inserted = SaveSystem.GetInt("balls_inserted");
            for (int i = 0; i < inserted; ++i) {
                balls[i].SetActive(true);
            }

            if (inserted == balls.Length) {
                DisableInteraction();
            }
        }
    }

    public override string GetLabel()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.GOLDEN_BALL))
            return InteractionLabels.USE_ITEM;
        else
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.GOLDEN_BALL)) {
            PlayerInventory.Instance().ConsumeSelectedItem();
            balls[inserted].SetActive(true);
            ++inserted;

            SaveSystem.SetInt("balls_inserted", inserted);
            audioSource.Play();

            if (inserted == balls.Length) {
                DisableInteraction();
                door.Unlock();
            }
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }
}
