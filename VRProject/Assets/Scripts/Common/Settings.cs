


public static class Settings 
{
    public const string INTERACTABLE_LAYER_NAME = "Interactable";

    public static bool dialogue = false;
    public static bool viewingPicture = false;
    public static bool paused {get {return dialogue || viewingPicture;}}

}