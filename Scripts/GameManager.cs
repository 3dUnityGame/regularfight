using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject archer;
    public GameObject rogue;
    public GameObject warrior;

    public GameObject toprakYol;
    public GameObject yesilYol;
    public GameObject tasYol;
    public GameObject colYol;

    public GameObject fri1;
    public GameObject fri2;
    public GameObject met1;
    public GameObject met2;
    public GameObject mush1;
    public GameObject mush2;
    public GameObject dio1;
    public GameObject dio2;
    public GameObject ven1;
    public GameObject ven2;
    public GameObject skelet;

    public GameObject coin;
    int coinCount;
    private float coinTime;

    private GameObject enemy11;
    private GameObject enemy12;
    private GameObject enemy21;
    private GameObject enemy22;

    private GameObject hero;
    private GameObject way1;
    private GameObject way2;
    private Transform cam;

    private float spawnTime;
    private float camPos = 31.0f;
    private bool firstWay = true;
    int heroNum;
    int mapNum;
    Vector3 wayForward;
    Vector3 enemyPos;
    Vector3 coinPos;

    private readonly GameObject[] c11 = new GameObject[3];
    private readonly GameObject[] c12 = new GameObject[3];
    private readonly GameObject[] c21 = new GameObject[3];
    private readonly GameObject[] c22 = new GameObject[3];

    void Start()
    {
        spawnTime = Time.time + 2.1f;
        coinTime = Time.time + 7;
        heroNum = PlayerPrefs.GetInt("C");
        mapNum = PlayerPrefs.GetInt("M");
        switch (heroNum)
        {
            case 1:
                hero = warrior;
                break;
            case 2:
                hero = rogue;
                break;
            case 3:
                hero = archer;
                break;
            default:
                break;
        }
        switch (mapNum)
        {
            case 1:
                way2 = yesilYol; //mushroom, metalon
                enemy11 = mush1;
                enemy12 = mush2;
                enemy21 = met1;
                enemy22 = met2;
                coinCount = 1;
                break;
            case 2:
                way2 = colYol; //skelet, fright
                enemy11 = skelet;
                enemy12 = skelet;
                enemy21 = fri1;
                enemy22 = fri2;
                coinCount = 3;
                break;
            case 3:
                way2 = tasYol; //metalon, fright
                enemy11 = met1;
                enemy12 = met2;
                enemy21 = fri1;
                enemy22 = fri2;
                coinCount = 4;
                break;
            case 4:
                way2 = toprakYol; //dioanea, venus
                enemy11 = dio1;
                enemy12 = dio2;
                enemy21 = ven1;
                enemy22 = ven2;
                coinCount = 5;
                break;
            default:
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            c11[i] = Instantiate(enemy11);
            c11[i].SetActive(false);
            c12[i] = Instantiate(enemy12);
            c12[i].SetActive(false);
            c21[i] = Instantiate(enemy21);
            c21[i].SetActive(false);
            c22[i] = Instantiate(enemy22);
            c22[i].SetActive(false);
        }
        cam = Camera.main.transform;

        way1 = Instantiate(way2, Vector3.zero, Quaternion.identity);
        way2 = Instantiate(way2, new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(hero, new Vector3(0, 0, -27), Quaternion.identity, cam);
        coin = Instantiate(coin);

        wayForward = new Vector3(0, 0, 120.0f);
        enemyPos = new Vector3(0, 0, 0);
        coinPos = new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        if (!HealthBar.hb.over)
        {
            if (cam.position.z > camPos)
            {
                if (firstWay)
                    way1.transform.position += wayForward;
                else
                    way2.transform.position += wayForward;
                camPos += 60;
                firstWay = !firstWay;
            }

            if (Time.time > coinTime)
            {
                for (int i = 0; i < coinCount; i++)
                {
                    coin.transform.GetChild(i).gameObject.SetActive(true);
                }
                coinPos.x = Random.Range(-5.0f, 5.0f);
                coinPos.z = cam.position.z + 37;
                coin.transform.position = coinPos;
                coinTime += 7;
            }


            if (Time.time > spawnTime)
            {
                enemyPos.x = Random.Range(-5.0f, 5.0f);
                enemyPos.z = cam.position.z + 35;
                if (Random.Range(1, 3) == 1)
                {
                    if (Random.Range(1, 3) == 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (c11[i].activeInHierarchy == false)
                            {
                                c11[i].transform.position = enemyPos;
                                c11[i].tag = enemy11.tag;
                                c11[i].SetActive(true);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (c12[i].activeInHierarchy == false)
                            {
                                c12[i].transform.position = enemyPos;
                                c12[i].tag = enemy12.tag;
                                c12[i].SetActive(true);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (Random.Range(1, 3) == 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (c21[i].activeInHierarchy == false)
                            {
                                c21[i].transform.position = enemyPos;
                                c21[i].tag = enemy21.tag;
                                c21[i].SetActive(true);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (c22[i].activeInHierarchy == false)
                            {
                                c22[i].transform.position = enemyPos;
                                c22[i].tag = enemy22.tag;
                                c22[i].SetActive(true);
                                break;
                            }
                        }
                    }
                }

                spawnTime += 2.1f;
            }
        }
    }
}
