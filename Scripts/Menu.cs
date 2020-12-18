using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Transform CharButtons;
    private Transform MapsButtons;

    void OnEnable()
    {
        CharButtons = transform.GetChild(6).GetChild(0);
        MapsButtons = transform.GetChild(7).GetChild(0);
        StartCoroutine(EnableHero());
        if(PlayerPrefs.GetInt("C") != 1)
        {
            CharButtons.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetString("2") == "T")
        {
            CharButtons.GetChild(1).GetChild(6).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("C") != 2)
                CharButtons.GetChild(1).GetChild(1).gameObject.SetActive(false);
            else
                CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Be Invisible!";

            CharButtons.GetChild(1).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("3") == "T")
        {
            CharButtons.GetChild(2).GetChild(6).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("C") != 3)
                CharButtons.GetChild(2).GetChild(1).gameObject.SetActive(false);
            else
                CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Send Arrows!";

            CharButtons.GetChild(2).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("M") != 1)
        {
            MapsButtons.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("5") == "T")
        {
            MapsButtons.GetChild(1).GetChild(6).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("M") != 2)
                MapsButtons.GetChild(1).GetChild(1).gameObject.SetActive(false);
            else
                MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 3 Gold Coins!";

            MapsButtons.GetChild(1).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("6") == "T")
        {
            MapsButtons.GetChild(2).GetChild(6).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("M") != 3)
                MapsButtons.GetChild(2).GetChild(1).gameObject.SetActive(false);
            else
                MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 4 Gold Coins!";

            MapsButtons.GetChild(2).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("7") == "T")
        {
            MapsButtons.GetChild(3).GetChild(6).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("M") != 4)
                MapsButtons.GetChild(3).GetChild(1).gameObject.SetActive(false);
            else
                MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 5 Gold Coins!";

            MapsButtons.GetChild(3).GetChild(3).gameObject.SetActive(false);
        }
    }

    private IEnumerator EnableHero()
    {
        yield return new WaitForSeconds(0.5f);
        switch (PlayerPrefs.GetInt("C"))
        {
            case 1:
                CharButtons.GetChild(0).GetComponent<Button>().onClick.Invoke();
                break;
            case 2:
                CharButtons.GetChild(1).GetComponent<Button>().onClick.Invoke();
                break;
            case 3:
                CharButtons.GetChild(2).GetComponent<Button>().onClick.Invoke();
                break;
            default:
                break;
        }
    }

    void Start()
    {
        CharButtons = transform.GetChild(6).GetChild(0);
        MapsButtons = transform.GetChild(7).GetChild(0);        
    }

    public void Next()
    {
        StartCoroutine(Deactivate(true));
        switch (PlayerPrefs.GetInt("M"))
        {
            case 1:
                MapsButtons.GetChild(0).GetComponent<Button>().onClick.Invoke();
                break;
            case 2:
                MapsButtons.GetChild(1).GetComponent<Button>().onClick.Invoke();
                break;
            case 3:
                MapsButtons.GetChild(2).GetComponent<Button>().onClick.Invoke();
                break;
            case 4:
                MapsButtons.GetChild(3).GetComponent<Button>().onClick.Invoke();
                break;
            default:
                break;
        }
    }

    public void Prev()
    {
        StartCoroutine(Deactivate(false));
        switch (PlayerPrefs.GetInt("C"))
        {
            case 1:
                CharButtons.GetChild(0).GetComponent<Button>().onClick.Invoke();
                break;
            case 2:
                CharButtons.GetChild(1).GetComponent<Button>().onClick.Invoke();
                break;
            case 3:
                CharButtons.GetChild(2).GetComponent<Button>().onClick.Invoke();
                break;
            default:
                break;
        }
    }

    private IEnumerator Deactivate(bool first)
    {
        yield return new WaitForSeconds(0.5f);
        if (first)
        {
            transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "MAPS";
            transform.GetChild(7).gameObject.SetActive(true);
            MapsButtons.GetChild(4).gameObject.SetActive(false);
            MapsButtons.GetComponent<Animator>().SetTrigger("N");
        }
        else
        {
            transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "CHARACTERS";
            transform.GetChild(6).gameObject.SetActive(true);
            CharButtons.GetChild(3).gameObject.SetActive(false);
            CharButtons.GetComponent<Animator>().SetTrigger("P");
        }

        yield return new WaitForSeconds(0.5f);

        if (first)
        {
            transform.GetChild(6).gameObject.SetActive(false);
            MapsButtons.GetChild(4).gameObject.SetActive(true);
        }

        else
        {
            transform.GetChild(7).gameObject.SetActive(false);
            CharButtons.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void BuyButton()
    {
        switch (PlayerPrefs.GetInt("buy"))
        {
            case 2:
                if(PlayerPrefs.GetInt("coin") >= 150)
                {
                    PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 150);
                    PlayerPrefs.SetString("2", "T");
                    CharButtons.GetChild(PlayerPrefs.GetInt("C") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("C", 2);
                    CharButtons.GetChild(1).GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(1).GetChild(6).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(3).gameObject.SetActive(true);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("coin") >= 500)
                {
                    PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 500);
                    PlayerPrefs.SetString("3", "T");
                    CharButtons.GetChild(PlayerPrefs.GetInt("C") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("C", 3);
                    CharButtons.GetChild(2).GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(2).GetChild(6).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(3).gameObject.SetActive(true);
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("coin") >= 50)
                {
                    PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 50);
                    PlayerPrefs.SetString("5", "T");
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 2);
                    MapsButtons.GetChild(1).GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(1).GetChild(6).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                }
                break;
            case 6:
                if (PlayerPrefs.GetInt("coin") >= 100)
                {
                    PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 100);
                    PlayerPrefs.SetString("6", "T");
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 3);
                    MapsButtons.GetChild(2).GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(2).GetChild(6).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                }
                break;
            case 7:
                if (PlayerPrefs.GetInt("coin") >= 150)
                {
                    PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 150);
                    PlayerPrefs.SetString("7", "T");
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 4);
                    MapsButtons.GetChild(3).GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(3).GetChild(6).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void Choose(int no)
    {
        switch (no)
        {
            case 1:
                CharButtons.GetChild(PlayerPrefs.GetInt("C") - 1).GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("C", 1);
                CharButtons.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                CharButtons.GetChild(3).gameObject.SetActive(true);
                CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Use Your Shield!";
                break;
            case 2:
                if (PlayerPrefs.GetString("2") == "T")
                {
                    CharButtons.GetChild(PlayerPrefs.GetInt("C") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("C", 2);
                    CharButtons.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(3).gameObject.SetActive(true);
                    CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Be Invisible!";
                }
                else
                {
                    CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Be Invisible!";
                    CharButtons.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("buy", 2);
                }
                break;
            case 3:
                if (PlayerPrefs.GetString("3") == "T")
                {
                    CharButtons.GetChild(PlayerPrefs.GetInt("C") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("C", 3);
                    CharButtons.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    CharButtons.GetChild(3).gameObject.SetActive(true);
                    CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Send Arrows!";
                }
                else
                {
                    CharButtons.GetChild(3).GetComponent<Text>().text = "Double Tap To Send Arrows!";
                    CharButtons.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("buy", 3);
                }
                break;
            case 4:
                MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("M", 1);
                MapsButtons.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                MapsButtons.GetChild(4).gameObject.SetActive(true);
                MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 1 Gold Coin!";
                break;
            case 5:
                if (PlayerPrefs.GetString("5") == "T")
                {
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 2);
                    MapsButtons.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 3 Gold Coins!";
                }
                else
                {
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 3 Gold Coins!";
                    MapsButtons.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("buy", 5);
                }
                break;
            case 6:
                if (PlayerPrefs.GetString("6") == "T")
                {
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 3);
                    MapsButtons.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 4 Gold Coins!";
                }
                else
                {
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 4 Gold Coins!";
                    MapsButtons.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("buy", 6);
                }
                break;
            case 7:
                if (PlayerPrefs.GetString("7") == "T")
                {
                    MapsButtons.GetChild(PlayerPrefs.GetInt("M") - 1).GetChild(1).gameObject.SetActive(false);
                    PlayerPrefs.SetInt("M", 4);
                    MapsButtons.GetChild(3).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    MapsButtons.GetChild(4).gameObject.SetActive(true);
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 5 Gold Coins!";
                }
                else
                {
                    MapsButtons.GetChild(4).GetComponent<Text>().text = "Catch 5 Gold Coins!";
                    MapsButtons.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("buy", 7);
                }
                break;
            default:
                break;
        }
    }
}
