using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // ������ �� ������ ������
    public float yOffset = 3f; // �������� ������ �� ��� Y, ����� �������� ��� ����� ������

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // ���������� ������ ������ ���� ����� ��������� ���������� ������
        float targetY = player.position.y + yOffset;
        if (Mathf.Abs(transform.position.y - targetY) > 0.01f) // ��������, 0.01f � ���������� �����������
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}
