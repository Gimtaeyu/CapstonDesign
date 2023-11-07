using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    public bool is_searchEnemy;
    
    private int W_number;
    private float W_Range_radius;

    public float W_atkDamage;
    public float W_cooltime;

    public bool W_isatkOn;
    public float knockbackSpeed;

    public int attackCount;
    Animator anim_attack;
    
    void Start()
    {
        attackCount = 3; 
        is_searchEnemy = false;
        knockbackSpeed = 5f;
        W_isatkOn = true;
        if (W_number == 0)
        {
            W_Range_radius = 1.5f;
            W_atkDamage = 5.0f;
            W_cooltime = 1.0f;
        }
        anim_attack = this.GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (anim_attack.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") &&
           anim_attack.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
        {
            W_isatkOn = false;
        }

        if (W_cooltime < 0)
        {
            W_isatkOn = true;
            attackCount = 3;
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
            if (W_isatkOn)
            {
                if (attackCount > 0)
                {
                    if (W_cooltime <= 0) //한번만 실행
                    {
                        anim_attack.SetTrigger("isAttack");
                        W_cooltime = 2.0f;
                    }



                }
                else
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
        }
    }

}
