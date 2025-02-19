using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока
    public Vector3 offset = new Vector3(0f, 2f, -7f); // Смещение камеры относительно игрока
    public float smoothSpeed = 0.125f; // Скорость плавного перемещения камеры
    private float y;
    private float z;

    private void Start()
    {
        y = transform.position.y;
        z = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Игрок не задан для камеры!");
            return;
        }

        // Вычисляем целевую позицию камеры
        Vector3 desiredPosition = new Vector3(player.position.x, y, z) + offset;

        // Плавно перемещаем камеру к целевой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}