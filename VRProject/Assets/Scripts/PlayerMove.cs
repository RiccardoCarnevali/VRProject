using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Settings.paused)
            return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);
    }
}
