using UnityEngine;
using UnityEngine.InputSystem;

public class EMPAction : MonoBehaviour
{
    [SerializeField]
    private uint requiredCharge;
    private uint currentEnergy;
    public uint RequiredCharge
    { get { return requiredCharge; } }
    public uint CurrentEnergy 
    { get { return currentEnergy; } }

    [SerializeField]
    private GameObject empObject;

    private void Update()
    {
        // EMP charge cheat
        if (InputSystem.actions["EMPCheat"].WasPressedThisFrame())
        {
            currentEnergy = requiredCharge;
        }

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
            currentEnergy = 0;
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
