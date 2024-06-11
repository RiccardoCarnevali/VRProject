using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : Interactable
{
    [SerializeField] private Dialogue _inspectionDialogue;
    [SerializeField] private Animator _animator;

    private bool _locked = true;

    public void Unlock()
    {
        SaveSystem.SetFlag("locker_unlocked");
        _locked = false;
    }

    public override string GetLabel()
    {
        if (_locked) return InteractionLabels.INSPECT;
        return InteractionLabels.INTERACT;
    }

    public override void Interact()
    {
        if (_locked)
            DialogueManager.Instance().StartDialogue(_inspectionDialogue);
        else
        {
            _animator.SetBool("Open", true);
            DisableInteraction();
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Settings.load && SaveSystem.CheckFlag("locker_unlocked"))
            _locked = false;
    }

}
