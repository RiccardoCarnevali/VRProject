using HighlightPlus;
using UnityEngine;

public class FanPuzzle : CameraAffected {
    [SerializeField] private float rotationSpeed = 90.0f;
    private AudioSource audioSource;
    [SerializeField] private Collider keyCollider;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        if (Settings.load && SaveSystem.CheckFlag("fan_puzzle_done")) {
            Win();
        }
    }

    void Update(){
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    protected override void OnCameraAffected() {
        SaveSystem.SetFlag("fan_puzzle_done");
        Win();
    }

    private void Win() {
        enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<HighlightEffect>().enabled = false;
        audioSource.Stop();
        keyCollider.enabled = true;
    }
}

