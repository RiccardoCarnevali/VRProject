using UnityEngine;

public class ScrewedBox : MonoBehaviour
{
    private int numberOfScrews = 4;
    private int screwsRemoved = 0;

    [SerializeField] private GameObject cover;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (Settings.load) {
            screwsRemoved = SaveSystem.GetInt("screws_removed");

            if (screwsRemoved == numberOfScrews)
                Destroy(cover);
        }
    }

    public void ScrewRemoved() {
        ++screwsRemoved;
        SaveSystem.SetInt("screws_removed", screwsRemoved);

        if (numberOfScrews == screwsRemoved) {
            Destroy(cover);
            audioSource.Play();
        }
    }
}
