using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public float sensitivityHor = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Settings.load) {
            Vector3 angle = transform.localEulerAngles;
            angle.y = SaveSystem.GetFloat("playerlook_y");
            transform.localEulerAngles = angle;
        }
        else {
            SaveSystem.SetFloat("playerlook_y", transform.localEulerAngles.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Settings.paused)
            return;
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        SaveSystem.SetFloat("playerlook_y", transform.localEulerAngles.y);
    }
}
