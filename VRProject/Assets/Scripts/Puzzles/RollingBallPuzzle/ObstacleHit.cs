using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private LayerMask invisibleLayer;

    private float visibleTime = 1;

    private void OnCollisionEnter(Collision collision) {
        if (Vector3.Dot(collision.rigidbody.velocity.normalized, collision.contacts[0].normal.normalized) > 0.8) {
            StartCoroutine(MakeVisible());
        }
    }

    private IEnumerator MakeVisible() {
        float visibleProgress = 0;
        gameObject.layer = (int) Mathf.Log(visibleLayer.value, 2);
        Renderer renderer = GetComponent<Renderer>();
        Color startColor = new Color(1, 1, 1, 0.8f);
        Color endColor = new Color(1, 1, 1, 0);

        while (visibleProgress < visibleTime) {
            visibleProgress += Time.deltaTime * Time.timeScale;
            renderer.material.color = Color.Lerp(startColor, endColor, visibleProgress / visibleTime);
            yield return null;
        }

        gameObject.layer = (int) Mathf.Log(invisibleLayer.value, 2);
        renderer.material.color = Color.white;
    }
}