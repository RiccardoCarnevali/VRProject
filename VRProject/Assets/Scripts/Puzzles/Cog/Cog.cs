using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public float degrees;
    private static readonly float s_duration = 1.5f;
    private bool _resetRotationEndAnimation = false;
    private Quaternion _startRotation;

    public void CallRotate()
    {
        Debug.Log("rotate");
        StartCoroutine(Rotate());
    }

    public IEnumerator Rotate()
    {
        _startRotation = transform.localRotation;
        float degreeZ = transform.localEulerAngles.z;
        float timeElapsed = 0f;
        while (timeElapsed < s_duration)
        {
            float newDegreeZ = Mathf.Lerp(degreeZ, degreeZ + degrees, timeElapsed / s_duration);
            timeElapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, newDegreeZ);
            yield return null;
        }
        if(_resetRotationEndAnimation)
        {
            transform.localRotation = _startRotation;
            _resetRotationEndAnimation = false;
        } else
        {
            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, degreeZ + degrees);
        }
    }

    public void ResetRotationEndAnimation() => _resetRotationEndAnimation = true;
}
