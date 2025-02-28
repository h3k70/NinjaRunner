using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player; // Ссылка на трансформ игрока
    public Vector3 offset = new Vector3(0f, 2f, -7f); // Смещение камеры относительно игрока
    public float smoothSpeed = 0.125f; // Скорость плавного перемещения камеры
    private float y;
    private float z;

    private void Start()
    {
        y = transform.position.y;
        z = transform.position.z;

        player.Died += OnDied;
    }

    private void OnDied()
    {
        offset = new Vector3(0, -8 + player.transform.position.y, 9);
        transform.LookAt(player.transform.position);
        smoothSpeed = 0.01f;
    }

    void LateUpdate()
    {
        // Вычисляем целевую позицию камеры
        Vector3 desiredPosition = new Vector3(player.transform.position.x, y, z) + offset;

        // Плавно перемещаем камеру к целевой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}