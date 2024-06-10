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
    [SerializeField] private GameObject _startCog;
    [SerializeField] private GameObject _endCog;
    [SerializeField] private LockerInteractable _lockerToOpen;
    [SerializeField] private Dialogue _solvedDialogue;

    private static readonly float _cogScaling = 0.4f;
    private int _chosenCogIndex;
    private List<CogInteractable> _cogBearings;
    private List<GameObject> _cogs;
    private bool _solved = false;

    public override string GetLabel()
    {
        return InteractionLabels.INTERACT;
    }

    public override void Interact()
    {
        if(_solved)
        {
            DialogueManager.Instance().StartDialogue(_solvedDialogue);
        }
        _camera.SetActive(true);
        CursorManager.ShowCursor();
        Settings.inPuzzle = true;
        if(_cogs.Count == 0)
            InitiatePuzzle();
        _canvas.SetActive(true);
        _cogs[0].SetActive(true);
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
            newCog.transform.parent = null;
            _cogs.Add(newCog);
            ScaleCogToSlot(newCog);
            newCog.SetActive(false);
        }
        _cogPile.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();
        
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
        _canvas.SetActive(false);
        CursorManager.HideCursor();
        Settings.inPuzzle = false;
        if(_cogs.Count != 0)
            _cogs[_chosenCogIndex].SetActive(false);
        foreach (CogInteractable bearing in _cogBearings)
            bearing.GetComponent<Renderer>().material = _defaultBearingMaterial;
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

    public bool CogsIntersect()
    {
        foreach (CogInteractable cog in _cogBearings)
        {
            if(cog.PlacedCog != null)
            {
                SphereCollider collider = cog.PlacedCog.GetComponent<SphereCollider>();
                foreach (CogInteractable other in _cogBearings)
                {
                    if (other.PlacedCog != null && cog.PlacedCog != null && other != cog)
                    {
                        SphereCollider otherCollider = other.PlacedCog.GetComponent<SphereCollider>();
                        if (Physics.ComputePenetration(otherCollider, otherCollider.gameObject.transform.position, otherCollider.gameObject.transform.rotation,
                            collider, cog.PlacedCog.transform.position,
                            cog.PlacedCog.transform.rotation, out _, out _))
                        {
                            Debug.Log(otherCollider.gameObject.transform.position + " " + otherCollider.center + " " + otherCollider.radius);
                            Debug.Log(cog.PlacedCog.transform.position + " " + cog.PlacedCog.GetComponent<SphereCollider>().center + " " + cog.PlacedCog.GetComponent<SphereCollider>().radius);
                            return true;
                        }
                    }
                }
                if (Physics.ComputePenetration(_startCog.GetComponent<SphereCollider>(), _startCog.transform.position, _startCog.transform.rotation,
                        collider, cog.PlacedCog.transform.position, cog.PlacedCog.transform.rotation, out _, out _))
                {
                    Debug.Log("intersects start");
                    return true;
                }

                if (Physics.ComputePenetration(_endCog.GetComponent<SphereCollider>(), _endCog.transform.position, _endCog.transform.rotation,
                            collider, cog.PlacedCog.transform.position, cog.PlacedCog.transform.rotation, out _, out _))
                {
                    Debug.Log("intersects end");
                    return true;
                }
            }
        }
        return false;
    }

    public void TrySolving()
    {
        StartCoroutine(PlayAudio());
        _startCog.GetComponent<Cog>().CallRotate();
        foreach(CogInteractable cog in _cogBearings)
        {
            if (!cog.CorrectCog())
            {
                ResetCogs();
                return; 
            }
            cog.PlacedCog.GetComponent<Cog>().CallRotate();
        }
        _endCog.GetComponent<Cog>().CallRotate();
        _lockerToOpen.Unlock();
        _solved = true;
    }

    private IEnumerator PlayAudio()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.1f);
        GetComponent<AudioSource>().Stop();
    }


    private void ResetCogs()
    {
        _startCog.GetComponent<Cog>().ResetRotationEndAnimation();
        _endCog.GetComponent<Cog>().ResetRotationEndAnimation();
        foreach (CogInteractable cog in _cogBearings)
        {
            if (cog.PlacedCog != null)
            {
                cog.PlacedCog.GetComponent<Cog>().ResetRotationEndAnimation();
            }
        }
    }

    public void AddCogBearing(CogInteractable cog)
    {
        _cogBearings.Add(cog);
        _cogBearings.Sort(delegate(CogInteractable cog1, CogInteractable cog2) 
            {
                return cog1.CogSlot.CompareTo(cog2.CogSlot);
            }
        );
    }
}
