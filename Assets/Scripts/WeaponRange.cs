using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    public bool is_searchEnemy;
    
    private int W_number;
    private float W_Range_radius;
    public float W_atk;
    public float W_cooltime;
    public bool W_isatkOn;
    public float knockbackSpeed;

    void Start()
    {
        is_searchEnemy = false;
        knockbackSpeed = 5f;
        W_isatkOn = true;
        if (W_number == 0)
        {
            W_Range_radius = 1.5f;
            W_atk = 2.0f;
            W_cooltime = 1.0f;
        }
        this.GetComponent<CircleCollider2D>().radius = W_Range_radius;
    }

    void Update()
    {
        
        if(W_cooltime < 0)
        {
            W_isatkOn = true;
        }
        else
        {
            W_cooltime -= Time.deltaTime;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            is_searchEnemy = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            is_searchEnemy = false;
        }
    }

}
