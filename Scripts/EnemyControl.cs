using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject dioShoot;
    public GameObject friShoot;

    private GameObject dio1;
    private GameObject dio2;
    private GameObject fri1;
    private GameObject fri2;

    private Animator anim;
    Vector3 vec;

    bool shoot = true;
    bool shoot1 = false;
    bool died = false;
    float shootTimer = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        vec = new Vector3(0, 0, 3.5f);
        if (gameObject.name == "fri1(Clone)" || gameObject.name == "fri2(Clone)")
        {
            fri1 = Instantiate(friShoot);
            fri1.SetActive(false);
            fri2 = Instantiate(friShoot);
            fri2.SetActive(false);
        }
        else if (gameObject.name == "dio1(Clone)" || gameObject.name == "dio2(Clone)")
        {
            dio1 = Instantiate(dioShoot);
            dio1.SetActive(false);
            dio2 = Instantiate(dioShoot);
            dio2.SetActive(false);
        }
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if(transform.parent != null)
            {
                transform.parent = null;
            }
            transform.position += vec * 2.5f * Time.deltaTime;
            died = true;
        }
        else
        {
            if (died)
            {
                died = false;
                shoot = true;
                shoot1 = false;
                shootTimer = 0;
            }
            if (shoot)
            {
                if (tag == "ranged")
                {
                    shootTimer += Time.deltaTime;
                    if(shootTimer < 0.5f)
                    {
                        transform.position += vec * Time.deltaTime;
                    }
                    else if(shootTimer < 1.2f)
                    {
                        transform.position += vec * 2 * Time.deltaTime;
                    }
                    else
                    {
                        transform.position += vec * 4 * Time.deltaTime;
                        if(!shoot1 && shootTimer > 2.5f)
                        {
                            Shoot(shoot1);
                            shoot1 = true;
                        }
                        else if(shootTimer > 3.3f)
                        {
                            Shoot(shoot1);
                            shoot = false;
                            shootTimer = 0;
                        }
                    }
                }
            }
            else if (shootTimer < 0.45f)
            {
                shootTimer += Time.deltaTime;
                transform.position += vec * 4 * Time.deltaTime;
            }
        }
    }

    void Shoot(bool firstShoot)
    {
        GetComponent<Animator>().SetTrigger("Attack");
        if (gameObject.name == "fri1(Clone)" || gameObject.name == "fri2(Clone)")
        {
            if (!firstShoot)
            {
                fri1.SetActive(true);
                fri1.transform.position = transform.position;
            }
            else
            {
                fri2.SetActive(true);
                fri2.transform.position = transform.position;
            }
        }
        else if (gameObject.name == "dio1(Clone)" || gameObject.name == "dio2(Clone)")
        {
            if (!firstShoot)
            {
                dio1.SetActive(true);
                dio1.transform.position = transform.position;
            }
            else
            {
                dio2.SetActive(true);
                dio2.transform.position = transform.position;
            }
        }
    }
}
