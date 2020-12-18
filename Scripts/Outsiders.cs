using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outsiders : MonoBehaviour
{
    private int health;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            other.gameObject.SetActive(false);
            health = HealthBar.hb.Damage();
            if(health <= 0)
            {
                HealthBar.hb.Die(transform.parent.GetChild(1).GetComponent<CharacterControllerSimple>().coinCount);
            }
        }
        else if (other.tag == "dead" || other.tag == "fri")
        {
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "ranged")
        {
            health = HealthBar.hb.Damage();
            if (health <= 0)
            {
                HealthBar.hb.Die(transform.parent.GetChild(1).GetComponent<CharacterControllerSimple>().coinCount);
            }
            StartCoroutine(Deactivate(other.gameObject));
        }
        else if(other.tag == "dio")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(true);
            other.transform.GetChild(2).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Deactivate(GameObject ranged)
    {
        ranged.GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        ranged.SetActive(false);
    }
}
