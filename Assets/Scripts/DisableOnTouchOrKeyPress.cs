using UnityEngine;

public class DisableOnTouchOrKeyPress : MonoBehaviour
{
    void Update()
    {
        // �������� �� ������� ������ �� ��������� �����������
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DisableObject();
        }

        // �������� �� ������� ������ A ��� D �� ����������
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            DisableObject();
        }
    }

    void DisableObject()
    {
        gameObject.SetActive(false); // ��������� ������� ������
    }
}
