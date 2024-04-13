using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGridGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // Префаб вашего объекта
    public int width = 10; // Ширина сетки
    public int height = 16; // Высота сетки
    public float spacingX = 0.8f; // Шаг по ширине
    public float spacingY = 0.8f; // Шаг по высоте
    public float startOpacity = 50f; // Начальная прозрачность
    public float opacityStep = 25f; // Шаг прозрачности

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
                // Создаем новый объект из префаба
                GameObject newObject = Instantiate(objectPrefab, transform.position + new Vector3(x * spacingX, y * spacingY, 0), Quaternion.identity);

                // Делаем созданный объект дочерним к текущему объекту
                newObject.transform.parent = transform;

                // Устанавливаем прозрачность материала объекта
                Color objectColor = newObject.GetComponent<Renderer>().material.color;
                objectColor.a = currentOpacity / 100f;
                newObject.GetComponent<Renderer>().material.color = objectColor;

                // Обновляем прозрачность для следующего объекта в текущем ряду
                currentOpacity = (currentOpacity == 50f) ? 25f : 50f;
            }

            // Меняем начальную прозрачность местами при старте нового ряда
            startOpacity = (startOpacity == 50f) ? 25f : 50f;
            // Обновляем прозрачность для следующего ряда
            currentOpacity = startOpacity;
        }
    }
}
