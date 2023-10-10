using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    float hInput;
    float vInput;
    Vector2 moveVec;

    private Animator anim;

    public Slider[] skill_ui;
    float[] maxValue = new float[4];
    float[] curValue = new float[4];

    void Start()
    {
        maxValue[0] = 1.0f; //A스킬 쿨타임 조정
        maxValue[1] = 5.0f; //S스킬 쿨타임 조정
        maxValue[2] = 10.0f; //D스킬 쿨타임 조정
        maxValue[3] = 50.0f; //F스킬 쿨타임 조정

        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        speed = 5.0f;
        for (int i = 0; i <skill_ui.Length; i++)
        {
            skill_ui[i].value = 1.0f;
        }
    }

    void Update()
    {
        Player_Move();
        Player_Skill();
    }


    private void Player_Skill()
    {
        if(Input.GetKeyDown("a"))
        {
            if(skill_ui[0].value == 1)
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
       for(int i =0; i <skill_ui.Length; i++)
        {
            curValue[i] += Time.deltaTime;
            skill_ui[i].value = curValue[i] / maxValue[i];
        }



    }


    private void Player_Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hInput = -1;
            this.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hInput = 1;
            this.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("isMoving", true);
        }
        else
        {
            hInput = 0;
        }
        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            vInput = 1;
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vInput = -1;
            anim.SetBool("isMoving", true);
        }
        else
        {
            vInput = 0;
        }

        if(hInput ==0 && vInput == 0)
        {
            anim.SetBool("isMoving", false);

        }

        moveVec = new Vector2(hInput, vInput);

        rigid.velocity = moveVec * speed;
    }
}
