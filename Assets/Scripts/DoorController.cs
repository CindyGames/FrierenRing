using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject connectedDoor; // ������ �� ��������� �����

    private bool isActive = true; // ����, ������� �� �����

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            if (connectedDoor != null)
            {
                // ��������� ��������� �����
                connectedDoor.SetActive(false);
            }


            // ����� ��������� ������� �����, ���� ����������
            // gameObject.SetActive(false);
        }
    }

    public void ActivateDoor()
    {
        // �������� �����
        gameObject.SetActive(true);
        isActive = true;
    }

    public void DeactivateDoor()
    {
        // ��������� �����
        gameObject.SetActive(false);
        isActive = false;
    }
}
