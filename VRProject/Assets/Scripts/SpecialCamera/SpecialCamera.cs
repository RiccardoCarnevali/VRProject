using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCamera : MonoBehaviour
{
    [SerializeField] private Camera specialCamera;
    [SerializeField] private GameObject pictureOriginal;
    private GameObject pictureFrame;
    [SerializeField] private float cooldown = 5f;
    private float lastPictureTime = -5f;

    private void Update() {
        if (Settings.paused)
            return;

        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastPictureTime > cooldown) {
            lastPictureTime = Time.time;
            StartCoroutine(TakePicture());
        }
    }

    public IEnumerator TakePicture()
    {
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
        yield return new WaitForSeconds(cooldown);
        ShowPicture();
        DestroyPicture();
    }

    public void ShowPicture() {
        Messenger<Texture>.Broadcast(MessageEvents.VIEW_PICTURE, pictureFrame.transform.Find("Picture").GetComponent<Renderer>().material.mainTexture);
    }

    public void DestroyPicture() {
        Destroy(pictureFrame);
    }
}
