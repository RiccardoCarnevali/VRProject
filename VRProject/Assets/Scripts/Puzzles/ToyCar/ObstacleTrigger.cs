using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out ToyCar toyCar)) {
            toyCar.Reset();
        }
    }
}
