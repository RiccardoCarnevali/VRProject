using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlaneControls planeControls;
    [SerializeField] private RollingBallPuzzleTrigger puzzleTrigger;
    [SerializeField] private Collider puzzleTriggerCollider;
    
    private void OnTriggerEnter(Collider other) {
        puzzleTrigger.enabled = false;
        puzzleTriggerCollider.enabled = false;
        puzzleTrigger.gameObject.layer = LayerMask.NameToLayer(Layers.DEFAULT_LAYER);
        StartCoroutine(planeControls.Win());
    }
}
