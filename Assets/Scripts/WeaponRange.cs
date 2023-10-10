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

    int attackCount;
    Animator anim_attack;
    
    void Start()
    {
        attackCount = 2; 
        is_searchEnemy = false;
        knockbackSpeed = 5f;
        W_isatkOn = true;
        if (W_number == 0)
        {
            W_Range_radius = 1.5f;
            W_atk = 2.0f;
            W_cooltime = 1.0f;
        }
        //this.GetComponent<PolygonCollider2D>().radius = W_Range_radius;
        anim_attack = this.GetComponentInParent<Animator>();
    }

    void Update()
    {
        
        if(W_cooltime < 0)
        {
            W_isatkOn = true;
            attackCount = 2;
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
            if (W_isatkOn)
            {
                
                attackCount--;
                Debug.Log(collision.name);
                if (W_cooltime <= 0) //한번만 실행
                {
                    anim_attack.SetTrigger("isAttack");
                    W_cooltime = 2.0f;
                }
                Vector2.Distance(this.transform.position, collision.transform.position);
                if (attackCount == 0)
                {
                    W_isatkOn = false;
                    
                }
            }
           
            
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
