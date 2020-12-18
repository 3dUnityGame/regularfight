using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    AudioSource au;
    public static HealthBar hb;
    public bool watched = false;

    private int can = 3;
    public int coi;
    private int allCoin;

    private float timer;
    private bool die = false;

    public int earnedCoin;
    public bool over = false;

    private Transform allText;
    private Transform Text;

    void Start()
    {
        au = GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("mute") == 1)
        {
            au.Stop();
        }
        hb = this;
        allText = transform.GetChild(4).GetChild(2);
        Text = transform.GetChild(4).GetChild(3);
    }

    public int Damage()
    {
        if(can > 0)
        {
            for (int i = 1; i < 7; i++)
            {
                transform.GetChild(can).GetChild(i).gameObject.SetActive(false);
            }
            can--;
        }
        return can;
    }

    public void Die(int coin)
    {
        au.Stop();
        over = true;
        coi = coin;
        earnedCoin = coin;

        allCoin = PlayerPrefs.GetInt("coin");
        allText.GetComponent<Text>().text = allCoin.ToString();

        PlayerPrefs.SetInt("coin", allCoin + coin);
        transform.GetChild(4).gameObject.SetActive(true);
        
        Text.GetComponent<Text>().text = "+" + coin.ToString();
        StartCoroutine(CoinText());
    }

    private IEnumerator CoinText()
    {
        yield return new WaitForSeconds(0.6f);
        die = true;
    }

    void Update()
    {
        if (die && coi > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer += 0.1f;
                coi--;
                allCoin++;
                Text.GetComponent<Text>().text = "+" + coi.ToString();
                allText.GetComponent<Text>().text = allCoin.ToString();
            }
        }
    }

    public void DoubleTheCoins()
    {
        coi += earnedCoin;
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + earnedCoin);
    }

    public void LoadScene(int num)
    {
        if (watched)
        {
            if (num == 1)
                SceneManager.LoadScene("MenuScene");
            else
                SceneManager.LoadScene("SampleScene");
        }
    }
}
