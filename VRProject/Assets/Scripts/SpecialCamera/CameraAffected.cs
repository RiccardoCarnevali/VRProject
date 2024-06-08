using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraAffected : MonoBehaviour
{
    [SerializeField] List<Renderer> renderers;
    public bool PlayerInside { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void OnCameraAffected();

    public IList<Renderer> GetRenderers()
    {
        return renderers.AsReadOnly();
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerInside |= other.GetComponent<CharacterController>() != null;
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerInside &= other.GetComponent<CharacterController>() == null;
    }
}
