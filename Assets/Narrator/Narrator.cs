using UnityEngine;

[CreateAssetMenu(fileName = "Narrator", menuName = "Scriptable Objects/Narrator")]

[System.Serializable]
public class Narrator : ScriptableObject
{

    [TextArea(2, 10)]

    public string[] lines;
    public int index = 0;
    
}
