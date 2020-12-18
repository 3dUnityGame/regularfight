using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public static EnergyBar eBar;

    private Vector3 increase = new Vector3(0.2f, 0, 0);
    private Vector3 reduce = new Vector3(1, 0, 0);

    void Start()
    {
        eBar = this;
    }

    public void SetEnergy()
    {
        transform.localScale += increase;
    }

    public void UseAbility()
    {
        transform.localScale -= reduce;
    }
}
