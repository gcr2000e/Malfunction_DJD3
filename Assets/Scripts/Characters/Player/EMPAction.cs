using UnityEngine;
using UnityEngine.InputSystem;

public class EMPAction : MonoBehaviour
{
    [SerializeField]
    private uint requiredCharge;
    private uint currentEnergy;

    [SerializeField]
    private GameObject empObject;

    private void Update()
    {
        if (InputSystem.actions["EMP"].WasPressedThisFrame())
        {
            UseAbility();
        }
    }

    private void UseAbility()
    {
        if (currentEnergy >= requiredCharge)
        {
            empObject.SetActive(true);
        }
    }

    public void GetEnergy()
    {
        currentEnergy++;
        if (currentEnergy > requiredCharge)
        {
            currentEnergy = requiredCharge;
        }
    }
}
