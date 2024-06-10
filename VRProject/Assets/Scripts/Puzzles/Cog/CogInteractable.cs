using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CogInteractable : MonoBehaviour 
{
    [SerializeField] private CogManager _cogManager;
    [SerializeField] private int _correctCog;
    private GameObject _placedCog = null;

    public GameObject PlacedCog { get => _placedCog; }
    public bool CorrectCog() 
    {
        if (_placedCog == null) return false;
        return _correctCog == _placedCog.GetComponent<Cog>().id; 
    } 

    public int CogSlot { get => _correctCog; }

    public void RemoveCog()
    {
        _cogManager.AddCog(_placedCog);
        CogManager.ScaleCogToSlot(_placedCog);
        _placedCog = null;
    }

    public IEnumerator PlaceCog(GameObject cog)
    {
        //TODO: logic to avoid intersections with other cogs
        _placedCog = cog;
        _placedCog.transform.position = transform.position;
        _placedCog.transform.parent = null;
        _cogManager.PlaceCog(_placedCog);
        CogManager.ScaleCogToBearing(_placedCog);
        _placedCog.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForFixedUpdate();
        _placedCog.GetComponent<MeshRenderer>().enabled = true;
        if (_cogManager.CogsIntersect())
        {
            //play sfx
            Debug.Log("Intersecting");
            RemoveCog();
        }
    }



    public void PlaceOrRemoveCog(GameObject cog)
    {
        Debug.Log(cog.activeSelf);
        if (_placedCog != null)
        {
            RemoveCog();
        }
        else
        {
            StartCoroutine(PlaceCog(cog));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cogManager.AddCogBearing(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
