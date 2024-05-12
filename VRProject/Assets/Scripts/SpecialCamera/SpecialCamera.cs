using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SpecialCamera : MonoBehaviour
{
    private Animator polaroidAnimator;
    [SerializeField] private Image cameraFlash;
    private float cameraFlashTime = 0.75f;

    [SerializeField] private Camera specialCamera;
    [SerializeField] private GameObject pictureOriginal;
    private GameObject pictureFrame;

    private bool animationPlaying = false;
    private float polaroidMovementTime = 3f;
    private float picturePrintTime = 5f;

    private void Start() {
        polaroidAnimator = GetComponent<Animator>();
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
		StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine() {
        float startTime = Time.time;

        Color white = Color.white;
        Color originalColor = cameraFlash.color;

        while (Time.time - startTime < cameraFlashTime) {
            cameraFlash.color = Color.Lerp(white, originalColor, (Time.time - startTime) / cameraFlashTime);
            yield return null;
        }
        cameraFlash.color = originalColor;
    }

    public void ShowPicture() {
        Messenger<Texture>.Broadcast(MessageEvents.VIEW_PICTURE, pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture);
    }

    public void DestroyPicture() {
        Destroy(pictureFrame);
    }
}
