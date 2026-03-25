using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            throw new MissingReferenceException("No Character Controller in Player");
    }
}
