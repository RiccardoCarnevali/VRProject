using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraAffected : CameraAffected
{
    protected override void OnCameraAffected()
    {
        Debug.Log("Affected");
    }
}
