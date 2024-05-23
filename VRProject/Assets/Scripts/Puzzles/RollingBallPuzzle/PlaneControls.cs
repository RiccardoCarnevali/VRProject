using System.Collections;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    [SerializeField] private Transform tiltingPartTransform;
    [SerializeField] private GameObject ball;
    private Rigidbody ballRigidbody;
    private Vector3 ballOriginalPosition;

    private bool canTilt = true;
    private readonly float tiltAngle = 20;
    private readonly float timeToTilt = 0.25f;
    [SerializeField] private GameObject horizontalBumpers;
    [SerializeField] private GameObject verticalBumpers;

    private float minTiltedTime = 1f;

    private void Start() {
        horizontalBumpers.SetActive(false);
        verticalBumpers.SetActive(false);
        ballRigidbody = ball.GetComponent<Rigidbody>();
        ballOriginalPosition = ball.transform.localPosition;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Reset();
            Settings.inPuzzle = false;
            gameObject.SetActive(false);
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

        Vector3 finalRotation = new Vector3(tiltAngle * vert, 0, tiltAngle * hor);
        canTilt = false;
        StartCoroutine(TiltPlane(Vector3.zero, finalRotation));
        StartCoroutine(ResetPlane());
    }

    private IEnumerator TiltPlane(Vector3 initialRotation, Vector3 finalRotation) {
        float rotationProgress = 0;
        
        //Smooth transition from untilted to tilted
        while (rotationProgress < timeToTilt) {
            rotationProgress += Time.deltaTime * Time.timeScale;
            tiltingPartTransform.localRotation = Quaternion.Lerp(Quaternion.Euler(initialRotation), Quaternion.Euler(finalRotation), rotationProgress / timeToTilt);
            yield return null;
        }
    }

    private IEnumerator ResetPlane() {

        //Waits a default minimum time to allow the ball to gain velocity through gravity (it can happen that right after tilting the plane the ball velocity still has magnitude 0)
        yield return new WaitForSeconds(minTiltedTime);

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
        tiltingPartTransform.localEulerAngles = Vector3.zero;
        ball.transform.SetLocalPositionAndRotation(ballOriginalPosition, Quaternion.identity);
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        canTilt = true;
        horizontalBumpers.SetActive(false);
        verticalBumpers.SetActive(false);
    }

    public void Win() {
        Reset();
        Settings.inPuzzle = false;
        gameObject.SetActive(false);
    }
}
