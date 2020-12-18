using UnityEngine;
using System.Collections;

public class CharacterControllerSimple : MonoBehaviour
{
    AudioSource au;
    public GameObject ok;
    public GameObject shield;
    int shiCount;
    
    Animator animator;

    private Material m1;
    private Color col;
    private Shader shader1;
    private Shader shader2;
    private Renderer rend;
    private bool shootable = true;

    private float angleMedian = 0;
    private float screenWidth;
    private float shootTime = 0;
    private bool walk = false;

    private int energy;
    float lastTime = -1;

    public int coinCount;
    private int health;

    void Start()
    {
        au = GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("mute") == 1)
        {
            au.mute = true;
        }
        if (gameObject.name == "Rog2(Clone)")
        {
            shader1 = Shader.Find("Standard");
            shader2 = Shader.Find("Mobile/Diffuse");
            rend = transform.GetChild(1).GetComponent<Renderer>();
            rend.material.shader = shader1;

            m1 = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            col = m1.color;
            col.a = 0.2f;
            rend.material.shader = shader2;
        }
        else if (tag == "arc")
            ok = Instantiate(ok, new Vector3(0, 0, -20), Quaternion.identity);
        else if (gameObject.name == "War1(Clone)")
        {
            shield = Instantiate(shield, transform.position, Quaternion.identity, transform.parent);
            shield.SetActive(false);
        }

        animator = gameObject.GetComponent<Animator>();
        screenWidth = Screen.width / 2;
    }

    void Update()
    {
        if (!HealthBar.hb.over)
        {
            angleMedian = 0;
            if (Input.touchCount == 1 && tag != "arc" && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Time.time - lastTime < 0.2f && energy == 5)
                {
                    EnergyBar.eBar.UseAbility();
                    energy = 0;
                    shootable = false;

                    if (gameObject.name == "War1(Clone)")
                    {
                        shield.SetActive(true);
                        StartCoroutine(Deactivate("w"));
                    }
                    else
                    {
                        rend.material.shader = shader1;
                        StartCoroutine(Deactivate("r"));
                    }
                }
                lastTime = Time.time;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.position.x > screenWidth)
                {
                    if (transform.position.x < 3.98f)
                        transform.position += new Vector3(9, 0, 0) * Time.deltaTime;

                    angleMedian = 45;
                    shootTime = 0;
                }
                else
                {
                    if (transform.position.x > -3.98f)
                        transform.position += new Vector3(-9, 0, 0) * Time.deltaTime;

                    angleMedian = 315;
                    shootTime = 0;
                }
                walk = true;
            }
            else
            {
                angleMedian = 0;
                if (tag == "arc")
                {
                    shootTime += Time.deltaTime;
                    if (shootTime > 0.7f || walk)
                    {
                        animator.SetTrigger("Attack_1");
                        BowAttack();
                        shootTime = 0;
                        walk = false;
                    }
                }
            }

            transform.eulerAngles = new Vector3(0, angleMedian, 0);
        }
        else
        {
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy" || other.tag == "ranged")
        {
            if(tag == "arc")
            {
                health = HealthBar.hb.Damage();
                if (health <= 0)
                {
                    HealthBar.hb.Die(coinCount);
                }
            }
            else
            {
                animator.SetTrigger("Attack_1");
                other.GetComponent<Animator>().SetTrigger("Die");
                other.tag = "dead";
                if (energy < 5)
                {
                    EnergyBar.eBar.SetEnergy();
                    energy++;
                }
            }
        }
        else if(shootable && (other.tag == "dio1" || other.tag == "fri"))
        {
            other.gameObject.SetActive(false);
            health = HealthBar.hb.Damage();
            if(health <= 0)
            {
                HealthBar.hb.Die(coinCount);
            }
        }
        else if(other.tag == "coin")
        {
            au.Play();
            coinCount++;
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Deactivate(string hero)
    {
        yield return new WaitForSeconds(4.7f);
        if(hero == "w")
        {
            shield.SetActive(false);
            shiCount = transform.parent.childCount;
            for (int i = 3; i < shiCount; i++)
            {
                transform.parent.GetChild(3).parent = null;
            }
        }
        else if(hero == "r")
        {
            rend.material.shader = shader2;
        }
        shootable = true;
    }

    public void BowAttack()
    {
        ok.transform.position = transform.position + Vector3.forward * 2 + Vector3.up;
    }
}
