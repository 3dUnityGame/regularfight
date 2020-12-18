using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int energy;
    float lastTime = -1;

    private Transform konum;
    private readonly GameObject[] arrows = new GameObject[3];
    private Vector3 pos1 = new Vector3(-3, 2, 0);
    private Vector3 pos2 = new Vector3(0, 2, 0);
    private Vector3 pos3 = new Vector3(3, 2, 0);

    void Start()
    {
        konum = GameObject.FindGameObjectWithTag("arc").transform;
        if(gameObject.name == "Arrow(Clone)")
        {
            arrows[0] = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            arrows[0].SetActive(false);
            arrows[1] = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            arrows[1].SetActive(false);
            arrows[2] = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            arrows[2].SetActive(false);
        }
    }

    void Update()
    {
        transform.position += new Vector3(0, 1, 56) * Time.deltaTime;

        if (Input.touchCount == 1 && gameObject.name == "Arrow(Clone)" && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Time.time - lastTime < 0.2f && energy == 5)
            {
                EnergyBar.eBar.UseAbility();
                energy = 0;

                pos1.z = konum.position.z;
                pos2.z = konum.position.z;
                pos3.z = konum.position.z;
                arrows[0].SetActive(true);
                arrows[0].transform.position = pos1;
                arrows[1].SetActive(true);
                arrows[1].transform.position = pos2;
                arrows[2].SetActive(true);
                arrows[2].transform.position = pos3;

                StartCoroutine(Deactivate(arrows[0]));
                StartCoroutine(Deactivate(arrows[1]));
                StartCoroutine(Deactivate(arrows[2]));
            }
            lastTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" || other.tag == "ranged")
        {
            other.GetComponent<Animator>().SetTrigger("Die");
            other.tag = "dead";
            transform.position = Vector3.zero;
            if(gameObject.name == "Arrow(Clone)" && energy < 5)
            {
                energy++;
                EnergyBar.eBar.SetEnergy();
            }
            StartCoroutine(Deactivate(other.gameObject));
        }
    }

    private IEnumerator Deactivate(GameObject enemy)
    {
        yield return new WaitForSeconds(1.5f);
        enemy.SetActive(false);
    }
}
