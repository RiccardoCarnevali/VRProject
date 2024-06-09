using UnityEngine;

public class ToyCar : CameraAffected
{
    private Vector3 startingPosition;

    private bool moving = false;
    private float speed = 0.1f;

    private void Start() {
        startingPosition = transform.position;
    }

    private void Update() {
        
        if (!moving)
            return;

        transform.Translate(speed * Time.deltaTime * -1 * transform.right);
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
