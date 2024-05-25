using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleHit : MonoBehaviour
{
    [SerializeField] private Material obstacleInvisible;
    [SerializeField] private Material obstacleVisible;

    private float visibleTime = 2f;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision) {
        if (Vector3.Dot(collision.rigidbody.velocity.normalized, collision.contacts[0].normal.normalized) > 0.8) {
            StartCoroutine(MakeVisible());
            audioSource.Play();
        }
    }

    private IEnumerator MakeVisible() {
        float visibleProgress = 0;

        gameObject.layer = LayerMask.NameToLayer(Layers.DEFAULT_LAYER);
        Renderer renderer = GetComponent<Renderer>();

        renderer.material = obstacleVisible;

        Color startColor = renderer.material.color;
        Color endColor = new Color(1, 1, 1, 0);

        while (visibleProgress < visibleTime) {
            visibleProgress += Time.deltaTime * Time.timeScale;
            renderer.material.color = Color.Lerp(startColor, endColor, visibleProgress / visibleTime);
            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer(Layers.INVISIBLE_LAYER);
        renderer.material = obstacleInvisible;
    }
}
