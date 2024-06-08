using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CameraAffected : MonoBehaviour
{
    public bool PlayerInside { get; private set; }

    public void TryAffect(Camera camera) {
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), GetComponent<Collider>().bounds))
        {
            OnCameraAffected();
        }
    }

    protected abstract void OnCameraAffected();
}
