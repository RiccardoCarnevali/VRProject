using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class SpecialCamera : MonoBehaviour
{
    private Animator polaroidAnimator;

    [SerializeField] private Camera specialCamera;
    [SerializeField] private GameObject pictureOriginal;
    private GameObject pictureFrame;

    private AudioSource audioSource;

    private bool animationPlaying = false;
    private float polaroidMovementTime = 3f;
    private float picturePrintTime = 5f;

    private void Start() {
        polaroidAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Settings.paused)
            return;

        if (Input.GetKeyDown(KeyCode.Q) && !animationPlaying) {
            Settings.takingPicture = true;
            StartCoroutine(TakePicture());
        }
    }

    public IEnumerator TakePicture()
    {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        animationPlaying = true;
        polaroidAnimator.SetTrigger("takePicture");

        yield return new WaitForSeconds(polaroidMovementTime);

        yield return new WaitForEndOfFrame();

        pictureFrame = Instantiate(pictureOriginal, pictureOriginal.transform.parent);

        int sqr = 1000;
	
        specialCamera.aspect = 1.0f;
        
        RenderTexture tempRT = new (sqr,sqr, 24 );

        specialCamera.targetTexture = tempRT;
	    specialCamera.Render();

        Texture2D image = new (specialCamera.targetTexture.width, specialCamera.targetTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = tempRT;
        image.ReadPixels(new Rect(0, 0, specialCamera.targetTexture.width, specialCamera.targetTexture.height), 0, 0);
        image.Apply();
        pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture = image;
        pictureFrame.GetComponent<Animator>().SetBool("out", true);
        yield return new WaitForSeconds(picturePrintTime);
        ShowPicture();
        DestroyPicture();
        animationPlaying = false;
    }

    private void CameraFlash() {
        audioSource.Play();
		Messenger.Broadcast(MessageEvents.CAMERA_FLASH);
    }

    public void ShowPicture() {
        Messenger<Texture>.Broadcast(MessageEvents.VIEW_PICTURE, pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture);
    }

    public void DestroyPicture() {
        Destroy(pictureFrame);
    }
}
