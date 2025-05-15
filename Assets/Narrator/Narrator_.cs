using UnityEngine;

[System.Serializable]
public class Narrator_ 
{
    public string name;
    [TextArea(3,10)]
    public string[] lines;
    public int index = 0;
}
