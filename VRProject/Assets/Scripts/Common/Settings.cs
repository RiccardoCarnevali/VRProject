public static class Settings 
{
    public static bool dialogue = false;
    public static bool takingPicture = false;
    public static bool pauseMenuOn = false;
    public static bool inventoryOn = false;
    public static bool inspecting = false;
    public static bool inPuzzle = false;
    public static bool gameStarting = true;
    public static bool padlockOn = false;
    public static bool inCutscene = false;

    public static bool paused {get {return dialogue || takingPicture || pauseMenuOn || inventoryOn || inspecting || inPuzzle || gameStarting || padlockOn || inCutscene; }}

    public static bool load = false;

    public static float defaultMouseSensitivity = 10;
    public static float defaultTextSpeed = 100;
    public static float defaultAmbienceVolume = 1f;
    public static float defaultSFXVolume = 1f;
}