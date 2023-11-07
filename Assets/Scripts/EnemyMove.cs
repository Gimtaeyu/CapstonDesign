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
    Vector3 dir;
    bool isAttacked;

    //드랍재료
    public GameObject[] drop_Material;
    

    void Start()
    {
        isAttacked = false;
           rBody2D = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        e_speed2 = 3.5f;
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
            Instantiate(drop_Material[0], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
       
        dir = target.position - transform.position;

        if (dir.x < 0) //왼쪽 쳐다보기
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        this.rBody2D.velocity = this.dir.normalized * e_speed2 * knockbackSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            if (collision.GetComponent<WeaponRange>().W_isatkOn)
            {
                if (!isAttacked)
                {
                    collision.GetComponent<WeaponRange>().attackCount--;
                    this.GetComponent<CircleCollider2D>().isTrigger = true;
                    isAttacked = true;
                    e_HP -= collision.GetComponent<WeaponRange>().W_atkDamage;
                    StartCoroutine(KnockbackRoutine(collision.GetComponent<WeaponRange>().knockbackSpeed));
                }
            }
        }
    }

    IEnumerator KnockbackRoutine(float knockbackSpeed)
    {
        this.knockbackSpeed = -knockbackSpeed * 2.0f ;
        yield return new WaitForSeconds(0.1f);
        this.knockbackSpeed = 1f;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<CircleCollider2D>().isTrigger = false;

        isAttacked = false;
    }
}
