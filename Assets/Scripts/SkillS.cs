using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillS : MonoBehaviour
{
    public List<GameObject> FoundObjects;
    GameObject target;
    public float shortDis;
    Animator anim;

    bool isAttackStart;
    float dir;
    void Awake()
    {
        this.transform.position = GameObject.FindWithTag("Player").transform.position;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // ù��°�� �������� ����ֱ� 

        target = FoundObjects[0]; // ù��°�� ���� 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
            {
                target = found;
                shortDis = Distance;
            }
        }
        isAttackStart = false;
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
        dir = target.transform.position.x - transform.position.x;

    }

    void Update()
    {
        if(!isAttackStart)
        {
            this.transform.position = GameObject.FindWithTag("Player").transform.position;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SkillS") &&
          anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            isAttackStart = true;
            FollowTarget();

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SkillS") &&
         anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            Debug.Log("�������");
            Destroy(gameObject);
        }
        

    }

    void FollowTarget()
    {
        if (target != null)
        {
            //Vector3 dir = target.transform.position - transform.position;
            //
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if(dir < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        this.transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.05f);


    }
}
