using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Наш объект.
    public GameObject Sphere;

    //Целочисленная переменная определяющая дистанцию перемещения.
    public int S;

    //Метод который будет выполнятся по нажатию нашей кнопки.
    public void OnButtonDown()
    {
        //Задаем нашей переменной целочисленное значение 1 или 2.
        S = Random.Range(1, 2);
        //Перемещаем наш объект на S расстояние по оси x.
        Sphere.transform.Translate(S, 0, 0);
    }
}
