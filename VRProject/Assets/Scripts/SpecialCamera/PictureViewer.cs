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
        if (pictureBackground.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            HidePicture();
    }

    private void ViewPicture(Texture pictureImg) {
        CursorManager.ShowCursor();
        pictureBackground.SetActive(true);
        picture.texture = pictureImg;
    }

    public void HidePicture() {
        CursorManager.HideCursor();
        Settings.takingPicture = false;
        pictureBackground.SetActive(false);
    }
}
