public class RollingBallPuzzleTrigger : Interactable
{
    RollingBallPuzzle rollingBallPuzzle;

    public override string GetLabel()
    {
        return "Interact";
    }

    public override void Interact()
    {
        rollingBallPuzzle.StartPuzzle();
    }

    void Start()
    {
        rollingBallPuzzle = GetComponent<RollingBallPuzzle>();
    }

}
