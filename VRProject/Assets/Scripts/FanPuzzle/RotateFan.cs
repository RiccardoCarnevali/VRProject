using HighlightPlus;
using UnityEngine;

public class RotateFan : CameraAffected {
    [SerializeField] private float rotationSpeed = 90.0f;

    void Start() {
        if (Settings.load && SaveSystem.CheckFlag("fan_puzzle_done")) {
            enabled = false;
            GetComponent<HighlightEffect>().enabled = false;
        }
    }

    protected override void OnCameraAffected() {
        enabled = false;
        GetComponent<HighlightEffect>().enabled = false;
        SaveSystem.SetFlag("fan_puzzle_done");
    }

    void Update(){
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}

