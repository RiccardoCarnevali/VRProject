using UnityEngine;

public class ScrewedBox : MonoBehaviour
{
    private float numberOfScrews = 4;

    [SerializeField] private GameObject cover;

    private AudioSource audioSource;

    public void ScrewRemoved() {
        --numberOfScrews;

        if (numberOfScrews == 0) {
            Destroy(cover);
            audioSource.Play();
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
