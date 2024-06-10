using UnityEngine;

public class ToyCar : CameraAffected
{
    [SerializeField] private AudioSource blipAudioSource;
    [SerializeField] private GameObject solutionNumber;

    [SerializeField] private GameObject frontWheels;
    [SerializeField] private GameObject backWheels;
    private Vector3 startingPosition;

    private bool moving = false;
    private float speed = 0.1f;
    private float wheelRotationSpeed = 120f;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        startingPosition = transform.position;

        if (Settings.load && SaveSystem.CheckFlag("toy_car_puzzle_won")) {
            GetComponent<HighlightController>().DisableHighlight();
            solutionNumber.SetActive(true);
            enabled = false;
        }
    }

    private void Update() {
        
        if (!moving)
            return;

        transform.Translate(speed * Time.deltaTime * transform.right);
        frontWheels.transform.Rotate(wheelRotationSpeed * Time.deltaTime * -1 * Vector3.forward);
        backWheels.transform.Rotate(wheelRotationSpeed * Time.deltaTime * -1 * Vector3.forward);
    }

    protected override void OnCameraAffected()
    {
        moving = !moving;

        if (moving)
            audioSource.Play();
        else
            audioSource.Stop();
    }

    public void Reset() {
        transform.position = startingPosition;
        Stop();
    }

    private void Stop() {
        moving = false;
        audioSource.Stop();
    }

    public void Win() {
        SaveSystem.SetFlag("toy_car_puzzle_won");
        Stop();
        GetComponent<HighlightController>().DisableHighlight();
        blipAudioSource.Play();
        solutionNumber.SetActive(true);
        enabled = false;
    }
}
