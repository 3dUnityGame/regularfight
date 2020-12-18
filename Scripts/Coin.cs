using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("coin").ToString();
    }

    public void Buy()
    {
        StartCoroutine(CoinText());
    }

    private IEnumerator CoinText()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Text>().text = PlayerPrefs.GetInt("coin").ToString();
    }
}
