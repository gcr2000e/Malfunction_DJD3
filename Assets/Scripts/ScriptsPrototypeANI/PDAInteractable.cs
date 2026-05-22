using UnityEngine;

public class PDAInteractable : MonoBehaviour
{
    [Header("PDA Content")]
    public string pdaTitle;

    [TextArea(5, 10)]
    public string pdaMessage;
}