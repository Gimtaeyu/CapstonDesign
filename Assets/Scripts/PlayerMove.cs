using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    float maxHp;
    public float curHp;


    Rigidbody2D rigid;
    public float hInput;
    float vInput;
    Vector2 moveVec;

    private Animator anim;
    
    public bool called_Inhence;

    public Slider hp_Bar;

    //DB
    public Player_Status Status_DB;
    List<Player_DB_Entity> status;

    private void Awake()
    {
        maxHp = 100;
        curHp = maxHp;
        status = Status_DB.Status;

        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        hp_Bar.value = 1;
    }

    void Start()
    {      
        called_Inhence = false; 

        speed = 5.0f + status[PlayerPrefs.GetInt("Speed_Level")].Inhence_Speed;


    }

    void Update()
    {

        anim.SetFloat("isDead", curHp);
        if(curHp >= 0)
        {
            hp_Bar.value = curHp / maxHp;
            Player_Move();

        }
        else
        {
            hInput = 0; vInput = 0;
            hp_Bar.value = 0;
            moveVec = new Vector2(hInput, vInput);

            rigid.velocity = moveVec * speed;
            this.GetComponent<CapsuleCollider2D>().enabled = false;

        }



        if (called_Inhence)
        {
            speed = 5.0f + status[PlayerPrefs.GetInt("Speed_Level")].Inhence_Speed;
            called_Inhence = false;
        }
    }

  


    private void Player_Move()
    {
        if(curHp < 0)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hInput = -1;
            //this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.localScale = new Vector3(3, 3, 4);
            anim.SetBool("isMoving", true);
            BackgroundOffset.instance.Leftmove();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hInput = 1;
           // this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.transform.localScale = new Vector3(-3, 3, 4);
            anim.SetBool("isMoving", true);
           BackgroundOffset.instance.Rightmove();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) //Drop
        {
            for(int i = 1; i < 4; i++)
            {
                if (collision.gameObject.name.Contains("" + i + ""))
                {
                    int tempInt = PlayerPrefs.GetInt("Drop_" + i + "");
                    PlayerPrefs.SetInt("Drop_" + i + "", ++tempInt);

                    Debug.Log(PlayerPrefs.GetInt("Drop_" + i + ""));

                    Destroy(collision.gameObject);
                }
            }          
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            curHp--;
        }
    }


}
