using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ball") {
            Debug.DrawLine(collision.transform.position, collision.transform.TransformPoint(collision.rigidbody.velocity).normalized, Color.green, 2);
            // if (GetComponent<Collider>().bounds.Contains(collision.transform.TransformPoint(collision.rigidbody.velocity))) {
            //     Debug.Log("yes");
            // }
        }
    }
}
