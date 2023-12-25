using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CreateManager : MonoBehaviour
{
    public Text[] drop_Text;
    int[] drop_Count = new int[3];


    void Start()
    {
        for(int i = 0; i <3; i++)
        {
            drop_Text[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();
            drop_Count[i] = PlayerPrefs.GetInt("Drop_" + (i + 1) + "");
        }
    }

    void Update()
    {
        
    }

    public void UnLockA()
    {
        if(drop_Count[0] >= 1 && drop_Count[1] >= 1 && drop_Count[2] >= 1 && PlayerPrefs.GetInt("SkillLock_0") != 1)
        {
            PlayerPrefs.SetInt("SkillLock_0", 1);
            for(int i =0; i <3; i++)
            {
                drop_Count[i]--;
                PlayerPrefs.SetInt("Drop_" + (i + 1) + "", drop_Count[i]);

                drop_Text[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();

            }
        }
        else
        {

        }
    }
    public void UnLockS()
    {
        if (drop_Count[0] >= 2 && drop_Count[1] >= 2 && drop_Count[2] >= 2 && PlayerPrefs.GetInt("SkillLock_1") != 1)
        {
            PlayerPrefs.SetInt("SkillLock_1", 1);
            for (int i = 0; i < 3; i++)
            {
                drop_Count[i] -= 2;
                PlayerPrefs.SetInt("Drop_" + (i + 1) + "", drop_Count[i]);

                drop_Text[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();

            }
        }
        else
        {

        }
    }
    public void UnLockD()
    {
        if (drop_Count[0] >= 3 && drop_Count[1] >= 3 && drop_Count[2] >= 1 && PlayerPrefs.GetInt("SkillLock_2") != 1)
        {
            PlayerPrefs.SetInt("SkillLock_2", 1);
            drop_Count[0] -= 3;
            drop_Count[1] -= 3;
            drop_Count[2] -= 1;

            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt("Drop_" + (i + 1) + "", drop_Count[i]);

                drop_Text[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();

            }
        }
        else
        {

        }
    }
    public void UnLockF()
    {
        if (drop_Count[0] >= 5 && drop_Count[1] >= 5 && drop_Count[2] >= 5 && PlayerPrefs.GetInt("SkillLock_3") != 1)
        {
            PlayerPrefs.SetInt("SkillLock_3", 1);
            

            for (int i = 0; i < 3; i++)
            {
                drop_Count[i] -= 5;

                PlayerPrefs.SetInt("Drop_" + (i + 1) + "", drop_Count[i]);

                drop_Text[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();

            }
        }
        else
        {

        }
    }

    public void ExitButton()
    {
        PlayerPrefs.SetString("FromSceneName", "Create");
        SceneManager.LoadScene("Main");

    }
}
