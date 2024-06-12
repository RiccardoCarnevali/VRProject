using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateCameraAffected : CameraAffected
{
    private static readonly float s_duration = 0.8f;
    [SerializeField] private Transform _pivot;
    [SerializeField] private GameObject _grate;
    [SerializeField] private GameObject _password;
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _openRotation;
    
    private string _openedFlag = "grate_open";
    private bool _opened = false;

    protected override void OnCameraAffected()
    {
        if (!_opened)
            StartCoroutine(Rotate());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Settings.load && SaveSystem.CheckFlag(_openedFlag))
        {
            _password.SetActive(true);
            _grate.transform.localPosition = _openPosition;
            _grate.transform.localEulerAngles = _openRotation;
        }
    }


    public IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1f);
        _opened = true;
        SaveSystem.SetFlag(_openedFlag);
        float timeElapsed = 0f;
        _password.SetActive(true);
        while (timeElapsed < s_duration)
        {
            timeElapsed += Time.deltaTime;
            _grate.transform.RotateAround(_pivot.position, Vector3.right, 150f * Time.deltaTime);
            yield return null;
        }
    }
}
