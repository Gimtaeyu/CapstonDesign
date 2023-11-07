using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Text money_txt;
    public static bool is_renewMoney;

    public GameObject Option_BG;
    public GameObject Upgrade_BG;
    void Start()
    {
        Option_BG.SetActive(false);
        Upgrade_BG.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Upgrade_BTN();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Create");
        }
    }

    public void Upgrade_BTN()
    {
        if (Upgrade_BG.activeSelf)
        {
            Upgrade_BG.SetActive(false);
        }
        else
        {
            if(!Option_BG.activeSelf)
            {
                Upgrade_BG.SetActive(true);
            }
        }
    }

    public void Option_BTN()
    {
        if(Option_BG.activeSelf)
        {
            Option_BG.SetActive(false);
        }
        else
        {
            if (!Upgrade_BG.activeSelf)
            {
                Option_BG.SetActive(true);
            }
            else
            {
                Upgrade_BG.SetActive(false);
            }
        }
    }
}
