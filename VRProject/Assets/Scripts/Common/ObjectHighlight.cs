using System.Linq;
using UnityEngine;


//This script is needed since the objects which came with the assets for the rooms do some magic fuckery at the start of the game and get combined together or something,
//meaning that setting the highlight material from the inspector doesn't do anything as it gets removed in play mode, so we set it after the game has started
//Lord forgive me
public class ObjectHighlight : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Renderer[] renderers;

    private Color startColor;
    private Color endColor;

    private float loopTimeSeconds = 1f;
    private float progress;
    private bool highlightRising = false;

    void Start()
    {
        endColor = highlightMaterial.color;
        startColor = endColor;
        startColor.a = 0;
        renderers = GetComponentsInChildren<Renderer>();

        progress = loopTimeSeconds;

        //Adds the the highlight material on top of the existing material
        foreach (Renderer renderer in renderers)
            renderer.materials = renderer.materials.Append(highlightMaterial).ToArray();
    }

    private void Update() {

        foreach (Renderer renderer in renderers)
            renderer.materials[renderer.materials.Length - 1].color = Color.Lerp(startColor, endColor, progress / loopTimeSeconds);

        if (highlightRising) {
            progress += Time.deltaTime * Time.timeScale;

            if (progress >= loopTimeSeconds)
                highlightRising = false;
        }
        else {
            progress -= Time.deltaTime * Time.timeScale;

            if (progress <= 0)
                highlightRising = true;
        }
    }

    public void DisableHighlight() {
        foreach (Renderer renderer in renderers) {
            //People on the internet told me this is O(n) and I have no reason to not believe them
            renderer.materials = renderer.materials.Reverse().Skip(1).Reverse().ToArray();
        }
        Destroy(this);
    }
}
