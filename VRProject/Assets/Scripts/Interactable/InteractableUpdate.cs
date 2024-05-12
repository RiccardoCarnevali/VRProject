using TMPro;
using UnityEngine;

public class InteractableUpdate : MonoBehaviour
{
    [SerializeField] private GameObject canInteractText;
    [SerializeField] private float interactDistance;

    private Camera _characterCamera;

    #region Unity messages
    void Start()
    {
        _characterCamera = Camera.main;
    }

    void Update()
    {
        if (Settings.paused)
            return;

        Ray ray = _characterCamera.ScreenPointToRay(new Vector3(_characterCamera.pixelWidth / 2, _characterCamera.pixelHeight / 2, 0));
        if(Physics.Raycast(ray, maxDistance: interactDistance, hitInfo: out RaycastHit hitInfo)) {
            bool hitInteractable = hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(Settings.INTERACTABLE_LAYER_NAME);

            //All objects marked with layer Interactable should have the Interactable component, but this checks just in case
            if (hitInteractable && hitInfo.transform.TryGetComponent(out Interactable interactable)) {
                canInteractText.GetComponent<TextMeshProUGUI>().text = interactable.GetLabel() + " (E)";
                canInteractText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E)) {
                    canInteractText.SetActive(false);
                    interactable.Interact();
                }
            }
        }
        else {
            canInteractText.SetActive(false);
        }

    }
    #endregion
}