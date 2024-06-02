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
    }

    [field: SerializeField] public string Description {get; private set;}
    [Searchable] public ItemType type;
    
}

