using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" || other.tag == "ranged")
        {
            other.gameObject.transform.parent = transform.parent;
        }
        else if (other.tag == "dio" || other.tag == "fri")
        {
            other.gameObject.SetActive(false);
        }
    }
}