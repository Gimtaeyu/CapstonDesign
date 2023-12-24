using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
