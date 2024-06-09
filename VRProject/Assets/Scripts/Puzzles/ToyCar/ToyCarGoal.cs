using UnityEngine;

public class ToyCarGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out ToyCar toyCar))
            Debug.Log("Car entered");
    }
}
