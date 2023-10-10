using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Text money_txt;
    public static bool is_renewMoney;

    public GameObject Option_bg;
    void Start()
    {
        Option_bg.SetActive(false);

        is_renewMoney = false;
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
        else
        {
            money_txt.text = PlayerPrefs.GetInt("Money").ToString();

        }
    }

    void Update()
    {
        if(is_renewMoney)
        {
            money_txt.text = PlayerPrefs.GetInt("Money").ToString();
            is_renewMoney = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Option_BTN();
        }
    }

    public void Upgrade_BTN()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void Option_BTN()
    {
        Option_bg.SetActive(true);
    }
}
