using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log("collision");
    }
}
