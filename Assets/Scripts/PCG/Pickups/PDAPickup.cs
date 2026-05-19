using UnityEngine;

public class PDAPickup : IPickup
{
    [SerializeField]
    private PDAData pdaData;

    protected override void OnPickup(GameObject player)
    {
        if (pdaData != null &&
            PDAUIManager.Instance != null)
        {
            PDAUIManager.Instance.ShowPDA(pdaData);
        }
    }

    public void SetPDAData(PDAData data)
    {
        pdaData = data;
    }
}
