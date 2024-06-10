using UnityEngine;

public class CameraAffectedTrigger : MonoBehaviour
{
    [SerializeField] private CameraAffected target;
    private bool registered = false;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out CharacterController charController) && !registered) {
            registered = true;
            Messenger<Camera>.AddListener(MessageEvents.AFFECT_WITH_CAMERA, target.TryAffect);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out CharacterController charController) && registered) {
            registered = false;
            Messenger<Camera>.RemoveListener(MessageEvents.AFFECT_WITH_CAMERA, target.TryAffect);
        }
    }
}
