using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    private float sensitivityVert = 9.0f;

    private float minimumVert = -60.0f;
    private float maximumVert = 60.0f;

    private float _rotationX;

    private void Start() {
        if (Settings.load) {
            Vector3 angle = transform.localEulerAngles;
            angle.x = SaveSystem.GetFloat("playerlook_x");
            transform.localEulerAngles = angle;
        }
        else {
            SaveSystem.SetFloat("playerlook_x", transform.localEulerAngles.x);
        }
        
        _rotationX = transform.localEulerAngles.x >= 360 + minimumVert ? transform.localEulerAngles.x - 360 : transform.localEulerAngles.x;
    }

    void Update()
    {
        if (Settings.paused)
            return;
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        
        float rotationY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        SaveSystem.SetFloat("playerlook_x", transform.localEulerAngles.x);
    }
}
