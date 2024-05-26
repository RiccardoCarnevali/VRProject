using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlaneControls planeControls;
    [SerializeField] private JoystickBaseInteractable joystickInteractable;
    [SerializeField] private Collider joystickInteractableCollider;
    
    private void OnTriggerEnter(Collider other) {
        joystickInteractable.enabled = false;
        joystickInteractableCollider.enabled = false;
        joystickInteractable.gameObject.layer = LayerMask.NameToLayer(Layers.DEFAULT_LAYER);
        StartCoroutine(planeControls.Win());
    }
}
