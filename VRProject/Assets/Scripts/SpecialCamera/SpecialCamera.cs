using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class SpecialCamera : MonoBehaviour
{
    private Animator polaroidAnimator;

    [SerializeField] private Camera specialCamera;
    [SerializeField] private GameObject pictureOriginal;
    private GameObject pictureFrame;

    private AudioSource audioSource;
    private float picturePrintTime = 5f;

    public bool lens = false;

    private void Start() {
        polaroidAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Settings.paused)
            return;

        if (Input.GetKeyDown(KeyCode.Q)) {
            TakePicture();
        }
    }

    public void TakePicture()
    {
        Settings.takingPicture = true;
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        polaroidAnimator.SetTrigger("takePicture");
    }

    private IEnumerator PrintImage() {
        yield return new WaitForEndOfFrame();

        //Instantiates the picture's frame inside the camera
        pictureFrame = Instantiate(pictureOriginal, pictureOriginal.transform.parent);

        //Saves what is being rendered by the special camera in a texture of size sqr x sqr
        int sqr = 1000;	
        specialCamera.aspect = 1.0f;
        RenderTexture tempRT = new (sqr,sqr, 24 );
        specialCamera.targetTexture = tempRT;
	    specialCamera.Render();
        Texture2D image = new (specialCamera.targetTexture.width, specialCamera.targetTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = tempRT;
        image.ReadPixels(new Rect(0, 0, specialCamera.targetTexture.width, specialCamera.targetTexture.height), 0, 0);
        image.Apply();

        //Places the saved image on the picture
        pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture = image;

        //Camera returns to its place
        yield return new WaitForSeconds(1);

        //Starts printing animation
        pictureFrame.GetComponent<Animator>().SetBool("out", true);
        yield return new WaitForSeconds(picturePrintTime);

        //Displays the image to the player
        Messenger<Texture>.Broadcast(MessageEvents.VIEW_PICTURE, pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture);
        Destroy(pictureFrame);
    }

    private IEnumerator AffectWorld() {
        Messenger<Camera>.Broadcast(MessageEvents.AFFECT_WITH_CAMERA, specialCamera, MessengerMode.DONT_REQUIRE_LISTENER);

        //Camera returns to its place
        yield return new WaitForSeconds(2);

        Settings.takingPicture = false;
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
    }

    private void CameraFlash() {
        audioSource.Play();

        if (!lens) {
            StartCoroutine(PrintImage());
            Messenger<Color>.Broadcast(MessageEvents.CAMERA_FLASH, Color.white);
        }
        else {
            StartCoroutine(AffectWorld());
            Messenger<Color>.Broadcast(MessageEvents.CAMERA_FLASH, new Color32(255, 156, 0, 255));
        }
    }
}
