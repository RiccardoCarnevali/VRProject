


public static class Settings 
{
    public const string INTERACTABLE_LAYER_NAME = "Interactable";

    public static bool dialogue = false;
    public static bool takingPicture = false;
    public static bool pauseMenuOn = false;
    public static bool paused {get {return dialogue || takingPicture || pauseMenuOn;}}

}