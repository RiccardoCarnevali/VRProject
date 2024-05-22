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
        return "Interact";
    }

    public override void Interact()
    {
        puzzle.StartPuzzle();
    }

}
