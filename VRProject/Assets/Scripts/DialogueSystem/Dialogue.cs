using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string speaker;

    [TextArea(order = 3)]
    public string[] sentences;
}
