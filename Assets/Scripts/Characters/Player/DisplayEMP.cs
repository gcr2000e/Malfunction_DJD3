using UnityEngine;
using UnityEngine.UI;

public class DisplayEMP : MonoBehaviour
{
    [SerializeField]
    private Slider empSlider;

    private EMPAction emp;

    private void Start()
    {
        emp = GetComponent<EMPAction>();

        empSlider.maxValue = emp.RequiredCharge;
        empSlider.value = emp.CurrentEnergy;
    }

    private void Update()
    {
        empSlider.value = emp.CurrentEnergy;
    }
}
