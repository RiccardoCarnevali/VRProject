using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _characterController;

    private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSFX;
    private float footstepLengthSeconds = 0.3f;
    private bool step = false;

    void Start()
    {
        _characterController = GetComponentInChildren<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Settings.paused)
            return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);

        if (_characterController.velocity.magnitude >= 2f && !step) {
            Debug.Log(_characterController.velocity.magnitude);
            audioSource.PlayOneShot(footstepSFX);
            StartCoroutine(WaitForFootsteps());
        }
    }

    private IEnumerator WaitForFootsteps() {
        step = true;
        yield return new WaitForSeconds(footstepLengthSeconds);
        step = false;
    }
}
