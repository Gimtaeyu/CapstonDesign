using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    /*
     * 플레이어 프리팹 이름들
       재료 : Drop_1, Drop_2, Drop_3
        돈 : Money
        강화레벨 : Atk_Level, Hp_Level, Speed_Level
        체력 : Cur_HP
      
     */

    public Text money_txt, atkLevel_txt, hpLevel_txt, speedLevel_txt;
    public static bool is_renewMoney;

    public GameObject Option_BG;
    public GameObject Upgrade_BG;

    PlayerMove player_Script;

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //치트
        //PlayerPrefs.SetInt("Drop_1", 10);
        //PlayerPrefs.SetInt("Drop_2", 10);
        //PlayerPrefs.SetInt("Drop_3", 10);

        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
        else
        {
            money_txt.text = PlayerPrefs.GetInt("Money").ToString();
        }

        if (!PlayerPrefs.HasKey("Atk_Level"))
        {
            PlayerPrefs.SetInt("Atk_Level", 0);
        }
        else
        {
            atkLevel_txt.text = PlayerPrefs.GetInt("Atk_Level").ToString();

        }
        if (!PlayerPrefs.HasKey("Hp_Level"))
        {
            PlayerPrefs.SetInt("Hp_Level", 0);
        }
        else
        {
            hpLevel_txt.text = PlayerPrefs.GetInt("Hp_Level").ToString();

        }

        if (!PlayerPrefs.HasKey("Speed_Level"))
        {
            PlayerPrefs.SetInt("Speed_Level", 0);
        }
        else
        {
            speedLevel_txt.text = PlayerPrefs.GetInt("Speed_Level").ToString();
        }
    }

    void Start()
    {
        Option_BG.SetActive(false);
        Upgrade_BG.SetActive(false);

        is_renewMoney = false;


        player_Script = GameObject.FindWithTag("Player").GetComponent<PlayerMove>(); 
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

    public void Inhence_Attack()
    {
        player_Script.called_Inhence = true;
        if (!PlayerPrefs.HasKey("Atk_Level"))
        {
            PlayerPrefs.SetInt("Atk_Level", 1);

            
        }
        else
        {
            int temp = PlayerPrefs.GetInt("Atk_Level");
            if(temp < 30)
            {
                temp++;
                int temp_Money = PlayerPrefs.GetInt("Money");
                temp_Money -= 3;

                PlayerPrefs.SetInt("Money", temp_Money);
                money_txt.text = PlayerPrefs.GetInt("Money").ToString();
            }
            atkLevel_txt.text = temp.ToString();
            PlayerPrefs.SetInt("Atk_Level", temp);
        }
        
    }

    public void Inhence_Hp()
    {
        player_Script.called_Inhence = true;

        if (!PlayerPrefs.HasKey("Hp_Level"))
        {
            PlayerPrefs.SetInt("Hp_Level", 1);
        }
        else
        {
            int temp = PlayerPrefs.GetInt("Hp_Level");
            if (temp < 30)
            {
                temp++;
                int temp_Money = PlayerPrefs.GetInt("Money");
                temp_Money -= 3;

                PlayerPrefs.SetInt("Money", temp_Money);
                money_txt.text = PlayerPrefs.GetInt("Money").ToString();

            }
            hpLevel_txt.text = temp.ToString();
            PlayerPrefs.SetInt("Hp_Level", temp);
        }
    }

    public void Inhence_Speed()  
    {
        player_Script.called_Inhence = true;

        if (!PlayerPrefs.HasKey("Speed_Level"))
        {
            PlayerPrefs.SetInt("Speed_Level", 1);
        }
        else
        {
            int temp = PlayerPrefs.GetInt("Speed_Level");
            if (temp < 30)
            {
                temp++;
                int temp_Money = PlayerPrefs.GetInt("Money");
                temp_Money -= 3;

                PlayerPrefs.SetInt("Money", temp_Money);
                money_txt.text = PlayerPrefs.GetInt("Money").ToString();
            }
            speedLevel_txt.text = temp.ToString();
            PlayerPrefs.SetInt("Speed_Level", temp);
        }
    }

    public void CreateStore()
    {
        SceneManager.LoadScene("Create");
    }
}
