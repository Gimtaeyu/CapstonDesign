using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    public Text[] drop_Count;

    void Start()
    {
        for(int i = 0; i < drop_Count.Length; i++)
        {
            drop_Count[i].text = PlayerPrefs.GetInt("Drop_" + (i + 1) + "").ToString();
        }
    }

    void Update()
    {
        
    }
}
