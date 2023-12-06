using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillF : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = this.transform.GetComponent<Animator>();
    }

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("SkillF") &&
          anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SkillF") &&
         anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {


            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy")
        //{
        //    Debug.Log(collision.name);
        //
        //
        //    collision.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        //
        //}

    }
}
