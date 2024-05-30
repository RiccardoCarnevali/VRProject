using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlaneControls planeControls;
    [SerializeField] private GameObject joystick;
    
    private void OnTriggerEnter(Collider other) {
        joystick.gameObject.layer = LayerMask.NameToLayer(Layers.DEFAULT_LAYER);
        StartCoroutine(planeControls.Win());
    }
}
