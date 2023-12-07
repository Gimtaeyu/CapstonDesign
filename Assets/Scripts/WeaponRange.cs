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
    public bool W_isAttacking;
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

    public GameObject S_Object;
    public GameObject F_Object;

    bool isA;
    bool isD;

    bool[] isLock = new bool[4];

    public Slider[] skill_ui;
    public GameObject[] lock_Icon;
    float[] maxValue = new float[4];
    float[] curValue = new float[4];

    private void Awake()
    {
        skill_D_duration = 10.0f;

        maxValue[0] = 1.0f; //A스킬 쿨타임 조정
        maxValue[1] = 5.0f; //S스킬 쿨타임 조정
        maxValue[2] = 10.0f; //D스킬 쿨타임 조정
        maxValue[3] = 5.0f; //F스킬 쿨타임 조정


        for(int i = 0; i < 4; i++)
        {
            if(PlayerPrefs.HasKey("SkillLock_"+i))
            {
                if (PlayerPrefs.GetInt("SkillLock_" + i) != 0)
                {
                    isLock[i] = false;              //스킬해금

                }
                else
                {
                    isLock[i] = true;

                }
            }
            else
            {
                PlayerPrefs.SetInt("SkillLock_" + i, 0);
                isLock[i] = true;
            }

            curValue[i] = maxValue[i];
            

            if (isLock[i])
            {
                lock_Icon[i].SetActive(true);

            }
            else
            {
                lock_Icon[i].SetActive(false);

            }
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
        W_isAttacking = false;
        playmove_scr = this.GetComponentInParent<PlayerMove>();

        isA = false;
        isD = false;

        anim_attack = player.GetComponent<Animator>();

        anim_Skill = this.GetComponent<Animator>();

    }

    void Update()
    {
        

        if (anim_attack.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") &&
          anim_attack.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
        {
            W_isAttacking = true;

        }

        if (anim_attack.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") &&
           anim_attack.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            W_isAttacking = false;
           
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
            if (!isLock[0])
            {
                if (skill_ui[0].value == 1)
                {
                    anim_Skill.SetTrigger("isSkillA");
                    isA = true;


                    curValue[0] = 0.0f;
                }
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (!isLock[1])
            {
                if (skill_ui[1].value == 1)
                {
                    Instantiate(S_Object, transform.position, Quaternion.identity);

                    curValue[1] = 0.0f;
                }
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (!isLock[2])
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
        }
        if (Input.GetKeyDown("f"))
        {
            if (!isLock[3])
            {
                if (skill_ui[3].value == 1)
                {
                    if (player.transform.rotation.y != 0)
                    {
                        GameObject temp = Instantiate(F_Object, transform.position + new Vector3(6.0f, 0, 0), player.transform.rotation);
                        temp.transform.SetParent(transform);
                    }
                    else
                    {
                        GameObject temp = Instantiate(F_Object, transform.position - new Vector3(6.0f, 0, 0), player.transform.rotation);
                        temp.transform.SetParent(transform);
                    }


                    curValue[3] = 0.0f;
                }
            }
        }
        for (int i = 0; i < skill_ui.Length; i++)
        {
            curValue[i] += Time.deltaTime;
            skill_ui[i].value = curValue[i] / maxValue[i];
        }

        //스킬 구현 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //스킬 D
        if (isA)
        {
            if (anim_Skill.GetCurrentAnimatorStateInfo(0).IsName("SkillA") &&
           anim_Skill.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f)
            {
                this.GetComponentInChildren<CircleCollider2D>().enabled = true;


            }


            if (anim_Skill.GetCurrentAnimatorStateInfo(0).IsName("SkillA") &&
           anim_Skill.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Debug.Log("꺼져");
                this.GetComponentInChildren<CircleCollider2D>().enabled = false;
                isA = false;


            }

        }
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
