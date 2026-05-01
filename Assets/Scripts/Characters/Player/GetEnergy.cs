using UnityEngine;

public class GetEnergy : MonoBehaviour
{
    private EMPAction emp;

    private void Start()
    {
        emp = GetComponentInParent<EMPAction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IEnemy>() != null)
        {
            emp.GetEnergy();
        }
    }
}
