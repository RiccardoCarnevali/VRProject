using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 90.0f; 

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}

