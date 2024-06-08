using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CogManager : Interactable
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private Material _defaultBearingMaterial;
    [SerializeField] private Material _chosenBearingMaterial;
    [SerializeField] private GameObject _cogPile;
    [SerializeField] private GameObject _chosenCogSlot;
    [SerializeField] private GameObject _canvas;

    private static readonly float _cogScaling = 0.4f;
    private int _chosenCogIndex;
    private List<CogInteractable> _cogBearings;
    private List<GameObject> _cogs;

    public override string GetLabel()
    {
        return InteractionLabels.INTERACT;
    }

    public override void Interact()
    {
        _camera.SetActive(true);
        CursorManager.ShowCursor();
        Settings.inPuzzle = true;
        InitiatePuzzle();
        _canvas.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InitiatePuzzle()
    {
        foreach (Transform cog in _cogPile.GetComponentInChildren<Transform>())
        {
            GameObject newCog = Instantiate(cog.gameObject, position: new(), new(), _chosenCogSlot.transform);
            newCog.transform.localPosition = new();
            newCog.SetActive(false);
            _cogs.Add(newCog);
            ScaleCogToSlot(newCog);
        }

        _cogs[0].SetActive(true);
    }

    void Awake()
    {
        _cogBearings = new();
        _cogs = new();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_camera.activeSelf) return;
        
        GameObject hitObject = null;
        Ray ray = _camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, maxDistance: 5f,
            layerMask: (1 << LayerMask.NameToLayer("CogBearing")), hitInfo: out RaycastHit hit);
        if (hit.collider != null)
        {
            hitObject = hit.collider.gameObject;
            _cogBearings.ForEach(bearing => { bearing.GetComponent<Renderer>().material = _defaultBearingMaterial; });
            hitObject.GetComponent<Renderer>().material = _chosenBearingMaterial;
        }
        
        if (Input.GetMouseButtonDown(0) && hitObject != null)
        {
            CogInteractable cogInteractable = hitObject.GetComponent<CogInteractable>();

            if(_cogs.Count == 0)
            {
                cogInteractable.RemoveCog();
            } else
            {
                GameObject cogToRemove = _cogs[_chosenCogIndex];
                cogInteractable.PlaceOrRemoveCog(cogToRemove);
                _chosenCogIndex = 0;
            }

            if (_cogs.Count > 0) _cogs[_chosenCogIndex].SetActive(true);
        }
        foreach (Button button in _chosenCogSlot.GetComponentsInChildren<Button>())
        {
            button.GetComponent<Image>().enabled = _cogs.Count > 0;
            button.enabled = _cogs.Count > 0;
        }
    }

    public void Exit()
    {
        _camera.SetActive(false);
        CursorManager.HideCursor();
        Settings.inPuzzle = false;

    }

    public void PreviousCog()
    {
        _cogs[_chosenCogIndex].SetActive(false);
        _chosenCogIndex++;
        _chosenCogIndex %= _cogs.Count;
        _cogs[_chosenCogIndex].SetActive(true);
    }

    public void NextCog()
    {
        _cogs[_chosenCogIndex].SetActive(false);
        _chosenCogIndex = _chosenCogIndex - 1 < 0 ? _cogs.Count - 1 : _chosenCogIndex - 1;
        _cogs[_chosenCogIndex].SetActive(true);
    }

    public void AddCog(GameObject cog)
    {
        _cogs.Add(cog);
        cog.transform.position = _chosenCogSlot.transform.position;
        if (_cogs.Count != 1) cog.SetActive(false);        
    }

    public void PlaceCog(GameObject cog)
    {
        _cogs.Remove(cog);
        _chosenCogIndex = 0;
        if(_cogs.Count != 0) _cogs[_chosenCogIndex].SetActive(true);
    }

    public static void ScaleCogToSlot(GameObject cog)
    {
        cog.transform.localScale *= _cogScaling;
    }

    public static void ScaleCogToBearing(GameObject cog)
    {
        cog.transform.localScale /= _cogScaling;
    }

    public bool CogsIntersect(Collider collider)
    {
       
        foreach(GameObject other in _cogs)
        {
            if (other.GetComponent<Collider>().bounds.Intersects(collider.bounds))
                return true;
        }
        return false;
    }

    public void Test() => Debug.Log("DragonballZ");

    public void AddCogBearing(CogInteractable cog) => _cogBearings.Add(cog);
}
