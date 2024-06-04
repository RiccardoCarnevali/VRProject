using System.Collections;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    [SerializeField] private Transform tiltingPartTransform;
    [SerializeField] private Transform joystickTransform;
    [SerializeField] private GameObject ball;
    private Rigidbody ballRigidbody;
    private Vector3 ballOriginalPosition;

    private bool canTilt = true;
    private readonly float tiltAngleDegrees = 20;
    private readonly float timeToTiltSeconds = 0.25f;
    [SerializeField] private GameObject horizontalBumpers;
    [SerializeField] private GameObject verticalBumpers;

    private float minTiltedTimeSeconds = 1f;

    private float winDelaySeconds = 1f;

    private bool won = false;

    private void Start() {
        horizontalBumpers.SetActive(false);
        verticalBumpers.SetActive(false);
        ballRigidbody = ball.GetComponent<Rigidbody>();
        ballOriginalPosition = ball.transform.localPosition;
    }

    void Update()
    {
        if (won)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Reset();
            ExitPuzzle();
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }

        if (!canTilt)
            return;

        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (vert != 0) {
            //Get the sign of the tilt (+ or -)
            vert = Mathf.RoundToInt(vert / Mathf.Abs(vert));

            //These prevent the ball from getting off course
            verticalBumpers.SetActive(true);
            hor = 0;
        }
        else if (hor != 0) {
            //Get the sign of the tilt (+ or -)
            hor = -Mathf.RoundToInt(hor / Mathf.Abs(hor));

            //These prevent the ball from getting off course
            horizontalBumpers.SetActive(true);
        }
        else {
            return;
        }

        Vector3 finalRotation = new Vector3(tiltAngleDegrees * vert, 0, tiltAngleDegrees * hor);
        canTilt = false;
        StartCoroutine(TiltPlane(Vector3.zero, finalRotation));
        StartCoroutine(ResetPlane());
    }

    private IEnumerator TiltPlane(Vector3 initialRotation, Vector3 finalRotation) {
        float rotationProgress = 0;
        
        //Smooth transition from untilted to tilted
        while (rotationProgress < timeToTiltSeconds) {
            rotationProgress += Time.deltaTime * Time.timeScale;
            Quaternion rotation = Quaternion.Lerp(Quaternion.Euler(initialRotation), Quaternion.Euler(finalRotation), rotationProgress / timeToTiltSeconds);
            tiltingPartTransform.localRotation = rotation;
            joystickTransform.localRotation = rotation;
            yield return null;
        }
    }

    private IEnumerator ResetPlane() {

        //Waits a default minimum time to allow the ball to gain velocity through gravity (it can happen that right after tilting the plane the ball velocity still has magnitude 0)
        yield return new WaitForSeconds(minTiltedTimeSeconds);

        //Wait until the ball stops after hitting an obstacle
        while (ballRigidbody.velocity.magnitude != 0)
            yield return null;

        //Tilt the plane back to its original rotation
        yield return TiltPlane(tiltingPartTransform.localEulerAngles, Vector3.zero);
        
        verticalBumpers.SetActive(false);
        horizontalBumpers.SetActive(false);
        canTilt = true;
    }

    private void Reset() {
        StopAllCoroutines();
        ResetMaze();
        ResetBall();
    }

    private void ResetMaze() {
        tiltingPartTransform.localEulerAngles = Vector3.zero;
        joystickTransform.localEulerAngles = Vector3.zero;
        canTilt = true;
        horizontalBumpers.SetActive(false);
        verticalBumpers.SetActive(false);
    }

    private void ResetBall() {
        ball.transform.SetLocalPositionAndRotation(ballOriginalPosition, Quaternion.identity);
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }

    public IEnumerator Win() {
        won = true;
        yield return new WaitForSeconds(winDelaySeconds);
        StopAllCoroutines();
        ResetMaze();

        Destroy(ball.GetComponent<Ball>());
        Destroy(ball.GetComponent<AudioSource>());
        Destroy(ball.GetComponent<Rigidbody>());
        ball.GetComponent<SphereCollider>().radius = 2;
        ExitPuzzle();
    }

    private void ExitPuzzle() {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inPuzzle = false;
        gameObject.SetActive(false);
    }
}
