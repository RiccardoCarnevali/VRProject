using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private RollingBallPuzzle rollingBallPuzzle;
    
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(rollingBallPuzzle.Win());
    }
}
