using UnityEngine;

public class ToyCar : CameraAffected
{
    [SerializeField] private GameObject frontWheels;
    [SerializeField] private GameObject backWheels;
    private Vector3 startingPosition;

    private bool moving = false;
    private float speed = 0.1f;
    private float wheelRotationSpeed = 90f;

    private void Start() {
        startingPosition = transform.position;
    }

    private void Update() {
        
        if (!moving)
            return;

        transform.Translate(speed * Time.deltaTime * transform.right);
        frontWheels.transform.Rotate(wheelRotationSpeed * Time.deltaTime * -1 * Vector3.forward);
        backWheels.transform.Rotate(wheelRotationSpeed * Time.deltaTime * -1 * Vector3.forward);
    }

    protected override void OnCameraAffected()
    {
        moving = !moving;
    }

    public void Reset() {
        transform.position = startingPosition;
        moving = false;
    }
}
