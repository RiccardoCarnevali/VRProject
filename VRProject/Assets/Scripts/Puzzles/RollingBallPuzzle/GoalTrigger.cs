using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlaneControls planeControls;
    [SerializeField] private Interactable joystick;
    
    private void OnTriggerEnter(Collider other) {
        joystick.DisableInteraction();
        StartCoroutine(planeControls.Win());
    }
}
