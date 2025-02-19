using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ������ �� ��������� ������
    public Vector3 offset = new Vector3(0f, 2f, -7f); // �������� ������ ������������ ������
    public float smoothSpeed = 0.125f; // �������� �������� ����������� ������
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
            Debug.LogWarning("����� �� ����� ��� ������!");
            return;
        }

        // ��������� ������� ������� ������
        Vector3 desiredPosition = new Vector3(player.position.x, y, z) + offset;

        // ������ ���������� ������ � ������� �������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}