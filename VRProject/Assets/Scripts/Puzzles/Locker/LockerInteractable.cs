using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : Interactable
{
    [SerializeField] private Dialogue _inspectionDialogue;

    private bool _locked = true;

    public void Unlock()
    {
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
            GetComponent<Animator>().SetBool("Open", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
