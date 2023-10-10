using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    Transform target;
    private float e_speed2;//일정속도
    private float knockbackSpeed;
    private float e_HP;
    Rigidbody2D rBody2D;
    Vector3 test;
    bool isAttacked;

    void Start()
    {
        isAttacked = false;
           rBody2D = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        e_speed2 = 4f;
        e_HP = 10.0f;
        knockbackSpeed = 1f;

    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.position, e_speed2 * Time.deltaTime);
        if(e_HP <= 0)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 50);
            GameManager.is_renewMoney = true;
            Destroy(this.gameObject);
        }


        test = target.position - transform.position;
        this.rBody2D.velocity = this.test.normalized * e_speed2 * knockbackSpeed;
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<WeaponRange>().W_isatkOn)
        {
            if (collision.tag == "Weapon")
            {
                if (!isAttacked)
                {
                    isAttacked = true;
                    e_HP -= collision.GetComponent<WeaponRange>().W_atk;
                    StartCoroutine(KnockbackRoutine(collision.GetComponent<WeaponRange>().knockbackSpeed));
                }
            }
        }
    }

    protected IEnumerator KnockbackRoutine(float knockbackSpeed)
    {
        this.knockbackSpeed = -knockbackSpeed * 1.5f ;
        yield return new WaitForSeconds(0.1f);
        this.knockbackSpeed = 1f;
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }
}
