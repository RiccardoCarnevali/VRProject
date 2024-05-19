using UnityEngine;

public class Slate : MonoBehaviour
{

    public enum SlateSize {
        BIG = 0,
        MEDIUM = 1,
        SMALL = 2,
    }

    public enum SlateShape {
        SQUARE,
        TRIANGLE,
        CIRCLE,
        STAR,
        PENTAGON,
        OCTAGON,
    }

    [SerializeField] private SlateSize size;
    [SerializeField] private SlateShape shape;

    public SlateSize Size {get {return size;}}
    public SlateShape Shape {get {return shape;}}
}
