using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CogInteractable : MonoBehaviour 
{
    [SerializeField] private CogManager _cogManager;
    [SerializeField] private GameObject _correctCog;
    private GameObject _placedCog = null;
    public bool CorrectCog { get => _correctCog == _placedCog; }

    public void RemoveCog()
    {
        _cogManager.AddCog(_placedCog);
        CogManager.ScaleCogToSlot(_placedCog);
        _placedCog = null;
    }

    public void PlaceCog(GameObject cog)
    {
        //TODO: logic to avoid intersections with other cogs
        _placedCog = cog;
        _placedCog.transform.position = transform.position;
        _cogManager.PlaceCog(_placedCog);
        CogManager.ScaleCogToBearing(_placedCog);
    }

    public bool CanPlaceCog(GameObject cog)
    {
        cog.transform.position = transform.position;
        CogManager.ScaleCogToBearing(cog);
        Debug.Log(_cogManager.CogsIntersect(cog.GetComponent<Collider>()));
        bool canPlace = Physics.OverlapSphere(cog.transform.position, cog.GetComponent<SphereCollider>().radius, layerMask: (1 << LayerMask.NameToLayer("Cog"))).Length == 1;
        Destroy(cog);
        return canPlace;
    }

    public void PlaceOrRemoveCog(GameObject cog)
    {
        if (_placedCog != null)
        {
            RemoveCog();
        }
        else
        {
            if(CanPlaceCog(Instantiate(cog, transform)))
                PlaceCog(cog);
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
