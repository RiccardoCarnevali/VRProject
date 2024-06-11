using HighlightPlus;
using UnityEngine;

public class ToyCar : CameraAffected
{
    [SerializeField] private AudioSource blipAudioSource;
    [SerializeField] private GameObject solutionNumber;

    [SerializeField] private GameObject frontWheels;
    [SerializeField] private GameObject backWheels;
    private Vector3 startingPosition;
    private Vector3 endingPosition = new Vector3(-0.825f, 1, 0);

    private bool moving = false;
    private float speed = 0.1f;
    private float wheelRotationSpeed = 120f;
    private bool won = false;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        startingPosition = transform.position;

        if (Settings.load && SaveSystem.CheckFlag("toy_car_puzzle_won")) {
            GetComponent<HighlightEffect>().enabled = false;
            solutionNumber.SetActive(true);
            transform.localPosition = endingPosition;
            won = true;
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
        if (won)
            return;
            
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
        GetComponent<HighlightEffect>().enabled = false;
        blipAudioSource.Play();
        solutionNumber.SetActive(true);
        won = true;
    }
}
