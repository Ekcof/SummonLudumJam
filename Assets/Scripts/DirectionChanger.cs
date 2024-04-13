using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    //public GameObject directionChanger;
    public bool rotateRight = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ray"))
        {
            MovingObject movingObject = other.GetComponent<MovingObject>();

            if (movingObject != null)
            {
                // Получаем текущее направление объекта
                int currentDirection = movingObject.GetCurrentDirection();

                // Определяем новое направление в зависимости от текущего и выбранного направления поворота
                int newDirection = GetNewDirection(currentDirection);

                Debug.Log("currentDirection%" + currentDirection + "newDirection%" + newDirection);
                // Меняем направление объекта
                movingObject.ChangeDirection(newDirection);

                // Поворачиваем объект на 90 градусов вокруг оси Z
                RotateObject();
            }
        }
    }

    private int GetNewDirection(int currentDirection)
    {
        if (rotateRight)
        {
            // Поворот направо
            switch (currentDirection)
            {
                case 0:
                    return 1;
                case 1:
                    return 3;
                case 2:
                    return 0;
                case 3:
                    return 2;
                default:
                    return currentDirection;
            }
        }
        else
        {
            // Поворот налево
            switch (currentDirection)
            {
                case 0:
                    return 2;
                case 1:
                    return 0;
                case 2:
                    return 3;
                case 3:
                    return 1;
                default:
                    return currentDirection;
            }
        }
    }

    private void RotateObject()
    {
        // Поворачиваем объект на 90 градусов вокруг оси Z
        transform.Rotate(Vector3.forward, 90f);
    }
}
