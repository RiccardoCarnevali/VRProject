using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureViewer : MonoBehaviour
{
    [SerializeField] private GameObject pictureBackground;
    [SerializeField] private RawImage picture;

    private void Start() {
        Messenger<Texture>.AddListener(MessageEvents.VIEW_PICTURE, ViewPicture);
    }

    private void OnDestroy() {
        Messenger<Texture>.RemoveListener(MessageEvents.VIEW_PICTURE, ViewPicture);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            HidePicture();
    }

    private void ViewPicture(Texture pictureImg) {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pictureBackground.SetActive(true);
        picture.texture = pictureImg;
    }

    public void HidePicture() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Settings.takingPicture = false;
        pictureBackground.SetActive(false);
    }
}
