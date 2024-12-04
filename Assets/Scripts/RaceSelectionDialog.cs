using UnityEngine;

public class RaceSelectionDialog : MonoBehaviour
{
    public GameObject raceSelectionPanel;  // UI-������ ��� ������ ����

    private void Start()
    {
        // �������� ������ �� ���������
        raceSelectionPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���������� �� ����� � ���������
        if (other.CompareTag("Player"))
        {
            // ��������� ������ ������ ����
            raceSelectionPanel.SetActive(true);
            // ������������� �������� ������, ���� �����
            other.GetComponent<MobileCharacterController>().stopMove = true;
        }
    }

}
