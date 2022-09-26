using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTool : MonoBehaviour
{

    RaycastHit hit;

    public Material[] material;
    public int x;
    Renderer ren;
    void Start()
    {
        x = -1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag.Contains("Paint"))
                {

                }

                if (hit.collider.gameObject.tag.Contains("Wall"))
                {
                    //Ŭ���� ������Ʈ�� ���� ��������
                    Renderer ren = hit.collider.gameObject.GetComponent<Renderer>();

                    //�Ž������� ����
                    ren.enabled = true;

                    //����Ʈ���� x���� ������Ʈ�� ������ ��
                    ren.sharedMaterial = material[x];

                    //x�� ����
                    //ChangeColor();
                }
            }
        }
    }

    //public void ChangeColor()
    //{
    //        if (x < 2)
    //        {
    //            x++;
    //        }
    //        else x = 0;
    //}
}
