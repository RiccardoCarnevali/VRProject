using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCameraAffected : CameraAffected
{
    [SerializeField] private CogManager _cogManager;
    protected override void OnCameraAffected()
    {
        if (!_cogManager.Solved)
        {
            StartCoroutine(SolvePuzzle());
        }
        
    }
    private IEnumerator SolvePuzzle()
    {
        yield return new WaitForSeconds(0.5f);  
        _cogManager.TrySolving();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
