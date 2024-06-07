using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Dialogue dialogue;

    void Start()
    {
        if (Settings.load && SaveSystem.CheckFlag("dialogue_" + id + "_triggered")) {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        SaveSystem.SetFlag("dialogue_" + id + "_triggered");
        DialogueManager.Instance().StartDialogue(dialogue);
        GetComponent<Collider>().enabled = false;
    }
}
