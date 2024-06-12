using UnityEngine;

[RequireComponent(typeof(ShapesPuzzle))]
public class ShapesPuzzleTrigger : Interactable
{
    private ShapesPuzzle puzzle;

    private void Start() {
        puzzle = GetComponent<ShapesPuzzle>();
    }

    public override string GetLabel()
    {
        return InteractionLabels.INTERACT;
    }

    public override void Interact()
    {
        puzzle.StartPuzzle();
    }

}
