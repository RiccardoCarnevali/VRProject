using UnityEngine;

public class Item: MonoBehaviour
{

    public enum ItemType
    {
        [InspectorName("Screwdriver")] SCREWDRIVER,
        [InspectorName("Screwdriver Handle")] SCREWDRIVER_HANDLE,
        [InspectorName("Screwdriver Tip")] SCREWDRIVER_TIP,
        [InspectorName("Golden ball")] GOLDEN_BALL,
        [InspectorName("Joystick")] JOYSTICK,
        [InspectorName("Scissors")] SCISSORS,
        [InspectorName("ScissorsIndexHalf")] SCISSORS_INDEX_HALF,
        [InspectorName("ScissorsThumbHalf")] SCISSORS_THUMB_HALF,
    }

    [field: SerializeField] public string Id {get; private set;}
    [field: SerializeField] public string Description {get; private set;}
    [field: SerializeField] public Vector3 Rotation {get; private set;}
    [field: SerializeField] public float Scale {get; private set;}
    [Searchable] public ItemType type;
    
}

