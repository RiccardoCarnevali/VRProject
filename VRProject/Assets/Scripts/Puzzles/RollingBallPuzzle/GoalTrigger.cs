using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlaneControls planeControls;
    [SerializeField] private RollingBallPuzzleTrigger puzzleTrigger;
    
    private void OnTriggerEnter(Collider other) {
        puzzleTrigger.enabled = false;
        planeControls.Win();
    }
}
