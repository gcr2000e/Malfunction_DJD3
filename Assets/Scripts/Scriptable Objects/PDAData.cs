using UnityEngine;

[CreateAssetMenu(fileName = "PDAData", menuName = "PDA/PDAData")]
public class PDAData : ScriptableObject
{
    public string pdaTitle;

    [TextArea(5, 20)]
    public string pdaText;
}
