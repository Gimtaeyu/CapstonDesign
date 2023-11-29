using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float curHp;


    Rigidbody2D rigid;
    float hInput;
    float vInput;
    Vector2 moveVec;

    private Animator anim;
    
    public bool called_Inhence;


    //DB
    public Player_Status Status_DB;
    List<Player_DB_Entity> status;

    private void Awake()
    {
        curHp = 100;
        status = Status_DB.Status;

        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {      
        called_Inhence = false; 

        speed = 5.0f + status[PlayerPrefs.GetInt("Speed_Level")].Inhence_Speed;


    }

    void Update()
    {
        Player_Move();



        if(called_Inhence)
        {
            speed = 5.0f + status[PlayerPrefs.GetInt("Speed_Level")].Inhence_Speed;
            called_Inhence = false;
        }
    }

  


    private void Player_Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hInput = -1;
            //this.GetComponent<SpriteRenderer>().flipX = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hInput = 1;
            //this.GetComponent<SpriteRenderer>().flipX = true;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
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

           //if(collision.gameObject.name.Contains("1"))
           //{
           //    int tempInt = PlayerPrefs.GetInt("Drop_1");
           //    PlayerPrefs.SetInt("Drop_1", tempInt++);
           //}
           //else if(collision.gameObject.name.Contains("2"))
           //{
           //    int tempInt = PlayerPrefs.GetInt("Drop_2");
           //    PlayerPrefs.SetInt("Drop_2", tempInt++);
           //}
           //else if (collision.gameObject.name.Contains("3"))
           //{
           //    int tempInt = PlayerPrefs.GetInt("Drop_3");
           //    PlayerPrefs.SetInt("Drop_3", tempInt++);
           //}
        }

        if(collision.gameObject.tag == "Enemy")
        {
            curHp--;
        }

    }


}
