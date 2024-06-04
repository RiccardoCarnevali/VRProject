using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolInteractable : Interactable
{
    [SerializeField] private Dialogue inspectionDialogue;
    [SerializeField] private Dialogue afterCutDialogue;
    [SerializeField] private AudioSource audioSource;

    public override string GetLabel()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.SCISSORS))
            return InteractionLabels.USE_ITEM;
        else
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.SCISSORS)) {
            audioSource.Play();
            Destroy(gameObject);
            DialogueManager.Instance().StartDialogue(afterCutDialogue);
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectionDialogue);
        }
    }
}
