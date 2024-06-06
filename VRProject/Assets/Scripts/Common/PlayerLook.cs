using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float sensitivityHor;

    // Start is called before the first frame update
    void Start()
    {
        sensitivityHor = PlayerPrefs.GetFloat("mouse_sensitivity", Settings.defaultMouseSensitivity);
        Messenger<float>.AddListener(MessageEvents.MOUSE_SENSITIVITY_CHANGED, OnSensitivityChanged);
        if (Settings.load) {
            Vector3 angle = transform.localEulerAngles;
            angle.y = SaveSystem.GetFloat("playerlook_y");
            transform.localEulerAngles = angle;
        }
        else {
            SaveSystem.SetFloat("playerlook_y", transform.localEulerAngles.y);
        }
    }

    private void OnDestroy() {
        Messenger<float>.RemoveListener(MessageEvents.MOUSE_SENSITIVITY_CHANGED, OnSensitivityChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (Settings.paused)
            return;
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        SaveSystem.SetFloat("playerlook_y", transform.localEulerAngles.y);
    }

    private void OnSensitivityChanged(float value) {
        sensitivityHor = value;
    }
}
