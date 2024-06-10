using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCameraAffected : CameraAffected
{
    [SerializeField] private CogManager _cogManager;
    public override void OnCameraAffected()
    {
        StartCoroutine(SolvePuzzle());
    }
    private IEnumerator SolvePuzzle()
    {
        yield return new WaitForSeconds(3.5f);  
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
