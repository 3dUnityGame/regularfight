using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    void Update()
    {
        if(!HealthBar.hb.over)
            transform.position += new Vector3(0, 0, 14) * Time.deltaTime;
    }
}
