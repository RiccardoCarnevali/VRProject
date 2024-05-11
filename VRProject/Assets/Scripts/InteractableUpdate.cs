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

        if(Physics.Raycast(
            origin: _characterCamera.transform.position, 
            direction: _characterCamera.transform.forward, 
            maxDistance: interactDistance, hitInfo: out RaycastHit hitInfo))
        {
            bool hitInteractable = hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(Settings.INTERACTABLE_LAYER_NAME);
            canInteractText.SetActive(hitInteractable);
            if (hitInteractable && Input.GetKeyDown(KeyCode.E)) {

                //All objects marked with layer Interactable should have the Interactable component, but this checks just in case
                if (hitInfo.transform.TryGetComponent(out Interactable interactable)) { 
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
