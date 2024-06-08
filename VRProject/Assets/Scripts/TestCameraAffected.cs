using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraAffected : CameraAffected
{
    public override void OnCameraAffected()
    {
        Debug.Log("Affected");
    }
}
