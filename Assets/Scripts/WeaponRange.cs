using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //플레이어 애니메이터
    public GameObject player;
    Animator anim_attack;


    //웨폰 에니메이터
    Animator anim_Skill;
    PlayerMove playmove_scr;

    //Skill
    float skill_D_duration;

    bool isA;
    bool isS;
    bool isD;
    bool isF;

    public Slider[] skill_ui;
    float[] maxValue = new float[4];
    float[] curValue = new float[4];

    private void Awake()
    {
        skill_D_duration = 10.0f;

        maxValue[0] = 1.0f; //A스킬 쿨타임 조정
        maxValue[1] = 5.0f; //S스킬 쿨타임 조정
        maxValue[2] = 10.0f; //D스킬 쿨타임 조정
        maxValue[3] = 50.0f; //F스킬 쿨타임 조정

        for(int i = 0; i < 4; i++)
        {
            curValue[i] = maxValue[i];
        }

        if (W_number == 0)
        {
            W_Range_radius = 1.5f;
            W_atkDamage = 3.0f;
            W_cooltime = 1.0f;
        }


        for (int i = 0; i < skill_ui.Length; i++)
        {
            
            skill_ui[i].value = 1.0f;
        }
    }

    void Start()
    {
        attackCount = 3; 
        is_searchEnemy = false;
        knockbackSpeed = 5f;
        W_isatkOn = true;
        playmove_scr = this.GetComponentInParent<PlayerMove>();

        isA = false;
        isS = false;
        isD = false;
        isF = false;

        anim_attack = player.GetComponent<Animator>();

        anim_Skill = this.GetComponent<Animator>();

    }

    void Update()
    {
        if (anim_attack.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") &&
           anim_attack.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
        {
            Debug.Log("afgasgasgdagasdhasdfh");
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

        Player_Skill();

    }


    private void Player_Skill()
    {
        if (Input.GetKeyDown("a"))
        {
            if (skill_ui[0].value == 1)
            {
                curValue[0] = 0.0f;
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (skill_ui[1].value == 1)
            {
                curValue[1] = 0.0f;
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (skill_ui[2].value == 1)
            {
                anim_Skill.SetTrigger("isSkillD");
                W_atkDamage = 10.0f;
                skill_D_duration = 0;

                isD = true;
                curValue[2] = 0.0f;
            }
        }
        if (Input.GetKeyDown("f"))
        {
            if (skill_ui[3].value == 1)
            {
                curValue[3] = 0.0f;
            }
        }
        for (int i = 0; i < skill_ui.Length; i++)
        {
            curValue[i] += Time.deltaTime;
            skill_ui[i].value = curValue[i] / maxValue[i];
        }

        //스킬 구현 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //스킬 D

        if (isD)
        {
            skill_D_duration -= Time.deltaTime;

            if(skill_D_duration < 0)
            {
                W_atkDamage = 3.0f;
                skill_D_duration = 10.0f;
                isD = false;
            }
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
                        Debug.Log("왜와이?>");
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
