using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BasicSword_WP : MonoBehaviour
{
    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 60f;
    public float radius = 20f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    public bool isCollision = false;

    void Update()
    {
        Vector2 interV = target.position - transform.position;

        // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
        if (interV.magnitude <= radius)
        {
            // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
            float dot = Vector2.Dot(interV.normalized, transform.forward);
            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);
            // angleRange�� ���ϱ� ���� degree�� ��ȯ
            float degree = Mathf.Rad2Deg * theta;
            OnDrawGizmos();

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
                Debug.Log("�浹");
            }
            else
            {
                isCollision = false;

            }

        }
        else
        {
            isCollision = false;

        }
    }

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
