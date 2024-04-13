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
                // �������� ������� ����������� �������
                int currentDirection = movingObject.GetCurrentDirection();

                // ���������� ����� ����������� � ����������� �� �������� � ���������� ����������� ��������
                int newDirection = GetNewDirection(currentDirection);

                Debug.Log("currentDirection%" + currentDirection + "newDirection%" + newDirection);
                // ������ ����������� �������
                movingObject.ChangeDirection(newDirection);

                // ������������ ������ �� 90 �������� ������ ��� Z
                RotateObject();
            }
        }
    }

    private int GetNewDirection(int currentDirection)
    {
        if (rotateRight)
        {
            // ������� �������
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
            // ������� ������
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
        // ������������ ������ �� 90 �������� ������ ��� Z
        transform.Rotate(Vector3.forward, 90f);
    }
}
