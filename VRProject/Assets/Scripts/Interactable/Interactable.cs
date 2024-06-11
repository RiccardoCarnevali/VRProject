using HighlightPlus;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
    public abstract string GetLabel();

    public void DisableInteraction() {
        gameObject.layer = LayerMask.NameToLayer(Layers.DEFAULT_LAYER);
        if (TryGetComponent(out HighlightEffect highlightEffect)) {
            highlightEffect.enabled = false;   
        }
    }
}
