using DG.Tweening;
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player; // Ссылка на трансформ игрока
    public Transform TargetMoveTransform; // Ссылка на трансформ игрока
    public Vector3 offset = new Vector3(0f, 2f, -7f); // Смещение камеры относительно игрока
    public Quaternion rotate; // Смещение камеры относительно игрока
    public float smoothSpeed = 0.125f; // Скорость плавного перемещения камеры
    public float y;
    public float z;

    void LateUpdate()
    {
        if (TargetMoveTransform != null)
        {

            // Плавное перемещение камеры к позиции Point
            transform.position = Vector3.Lerp(transform.position, TargetMoveTransform.position, 1 * Time.deltaTime);

            // Плавный поворот камеры к повороту Point
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetMoveTransform.rotation, 1 * Time.deltaTime);
            return;
        }

        // Вычисляем целевую позицию камеры
        Vector3 desiredPosition = new Vector3(player.transform.position.x, y, z) + offset;

        // Плавно перемещаем камеру к целевой позиции
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.rotation = rotate;
    }
}