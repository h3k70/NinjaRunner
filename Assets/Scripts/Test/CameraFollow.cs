using DG.Tweening;
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player; // ������ �� ��������� ������
    private Transform _targetMoveTransform; // ������ �� ��������� ������
    public Vector3 offset = new Vector3(0f, 2f, -7f); // �������� ������ ������������ ������
    public Quaternion rotate; // �������� ������ ������������ ������
    public float smoothSpeed = 0.125f; // �������� �������� ����������� ������
    public float y;
    public float z;

    public Transform TargetMoveTransform { get => _targetMoveTransform; set => _targetMoveTransform = value; }

    void LateUpdate()
    {
        if (_targetMoveTransform != null)
        {

            // ������� ����������� ������ � ������� Point
            transform.position = Vector3.Lerp(transform.position, _targetMoveTransform.position, 1 * Time.deltaTime);

            // ������� ������� ������ � �������� Point
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetMoveTransform.rotation, 1 * Time.deltaTime);
            return;
        }

        // ��������� ������� ������� ������
        Vector3 desiredPosition = new Vector3(player.transform.position.x, y, z) + offset;

        // ������ ���������� ������ � ������� �������
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.rotation = rotate;
    }
}