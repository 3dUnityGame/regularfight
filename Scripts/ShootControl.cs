using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    private Vector3 shoot = new Vector3(0, 0, -9);
    void Update()
    {
        if(!HealthBar.hb.over)
            transform.position += shoot * Time.deltaTime;
    }
}