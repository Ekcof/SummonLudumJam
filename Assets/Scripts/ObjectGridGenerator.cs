using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGridGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // ������ ������ �������
    public int width = 10; // ������ �����
    public int height = 16; // ������ �����
    public float spacingX = 0.8f; // ��� �� ������
    public float spacingY = 0.8f; // ��� �� ������
    public float startOpacity = 50f; // ��������� ������������
    public float opacityStep = 25f; // ��� ������������

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float currentOpacity = startOpacity;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // ������� ����� ������ �� �������
                GameObject newObject = Instantiate(objectPrefab, transform.position + new Vector3(x * spacingX, y * spacingY, 0), Quaternion.identity);

                // ������ ��������� ������ �������� � �������� �������
                newObject.transform.parent = transform;

                // ������������� ������������ ��������� �������
                Color objectColor = newObject.GetComponent<Renderer>().material.color;
                objectColor.a = currentOpacity / 100f;
                newObject.GetComponent<Renderer>().material.color = objectColor;

                // ��������� ������������ ��� ���������� ������� � ������� ����
                currentOpacity = (currentOpacity == 50f) ? 25f : 50f;
            }

            // ������ ��������� ������������ ������� ��� ������ ������ ����
            startOpacity = (startOpacity == 50f) ? 25f : 50f;
            // ��������� ������������ ��� ���������� ����
            currentOpacity = startOpacity;
        }
    }
}
