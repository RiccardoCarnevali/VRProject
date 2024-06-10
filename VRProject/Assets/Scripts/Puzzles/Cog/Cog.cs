using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public float degrees;
    private static readonly float s_duration = 1.5f;
    private bool _resetRotationEndAnimation = false;

    public void CallRotate()
    {
        Debug.Log("rotate");
        StartCoroutine(Rotate());
    }

    public IEnumerator Rotate()
    {
        float degreeZ = transform.rotation.eulerAngles.z;
        float timeElapsed = 0f;
        while (timeElapsed < s_duration)
        {
            float newDegreeZ = Mathf.Lerp(degreeZ, degreeZ + degrees, timeElapsed / s_duration);
            timeElapsed += Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, newDegreeZ);
            yield return null;
        }
        if(_resetRotationEndAnimation)
        {
            transform.rotation = Quaternion.Euler(0f,0f,0f);
            _resetRotationEndAnimation = false;
        } else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, degreeZ + degrees);
        }
    }

    public void ResetRotationEndAnimation() => _resetRotationEndAnimation = true;
}
