using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //��� ������.
    public GameObject Sphere;

    //������������� ���������� ������������ ��������� �����������.
    public int S;

    //����� ������� ����� ���������� �� ������� ����� ������.
    public void OnButtonDown()
    {
        //������ ����� ���������� ������������� �������� 1 ��� 2.
        S = Random.Range(1, 2);
        //���������� ��� ������ �� S ���������� �� ��� x.
        Sphere.transform.Translate(S, 0, 0);
    }
}
