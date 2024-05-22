using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    private bool canRotate = true;
    [SerializeField] private Transform ballTransform;

    private float rotation = 20;
    private Vector3 originalRotation;

    private void Start() {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {

        if (!canRotate)
            return;

        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (vert != 0) {
            transform.Rotate(rotation * vert / Mathf.Abs(vert), 0, 0);
            canRotate = false;
        }   
        else if (hor != 0) {

            transform.Rotate(0, 0, -rotation * hor / Mathf.Abs(hor));
            canRotate = false;
        }
    }

    public void HitObstacle() {
        canRotate = true;
        transform.localEulerAngles = originalRotation;
    }
}
