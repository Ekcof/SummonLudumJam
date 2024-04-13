using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // Скорость движения объекта
    [SerializeField] private int directionY = 0; // Направление движения (1 - Вверх, -1 - вниз)
    [SerializeField] private int directionX = 0; // Направление движения (1 - Вправо, -1 - влево)

    private void Start()
    {
        SetDefaultDirection();
    }
    /*private void Awake()
    {
        SetDefaultDirection();
    }*/

    private void FixedUpdate()
    {
        MoveObject();
    }

    void MoveObject()
    {
        transform.Translate(new Vector2(directionX * speed * Time.deltaTime, directionY * speed * Time.deltaTime));
    }

    public void ChangeDirection(int direction)
    {
        switch (direction)
        {
            case 0:
                SetDefaultDirection();
                break;
            case 1:
                directionY = 0;
                directionX = 1;
                break;
            case 2:
                directionY = 0;
                directionX = -1;
                break;
            case 3:
                directionY = -1;
                directionX = 0;
                break;
        }
    }

    public int GetCurrentDirection()
    {
        if (directionY == 1 && directionX == 0) return 0; // Стандартное направление
        if (directionY == 0 && directionX == 1) return 1; // Вправо
        if (directionY == 0 && directionX == -1) return 2; // Влево
        if (directionY == -1 && directionX == 0) return 3; // Вниз

        return -1; // В случае ошибки
    }

    private void SetDefaultDirection()
    {
        directionY = 1;
        directionX = 0;
    }

}
