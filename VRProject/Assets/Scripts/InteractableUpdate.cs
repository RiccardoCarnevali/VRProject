using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableUpdate : MonoBehaviour
{
    [SerializeField] GameObject CanInteractText;

    private Camera _characterCamera;

    #region Unity messages
    void Start()
    {
        _characterCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(
            origin: _characterCamera.transform.position, 
            direction: _characterCamera.transform.forward, 
            maxDistance: float.PositiveInfinity, hitInfo: out RaycastHit hitInfo))
            {
                bool hitInteractable = hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(Settings.INTERACTABLE_LAYER_NAME);
                CanInteractText.SetActive(hitInteractable);
            }
    }
    #endregion
}
