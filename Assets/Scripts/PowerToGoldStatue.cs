using System.Collections;
using TMPro;
using UnityEngine;

public class PowerToGoldStatue : MonoBehaviour
{
    public TextMeshProUGUI goldText; // ����� ��� ����������� ����������� ������

    private bool isActivated = false; // ��������, �������������� �� ������

    void Start()
    {
        if (goldText != null)
        {
            goldText.gameObject.SetActive(false); // �������� ����� � ������� � ������
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� ����� �������� ������
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // ��������, ��� ������ ���� ������������
            StartCoroutine(ActivateStatueAfterDelay(other));
            other.GetComponent<MobileCharacterController>().stopMove = true;
        }
    }

    // ����������� ��� ��������� ������ � ����������� ���� � ������ ����� 2 �������
    IEnumerator ActivateStatueAfterDelay(Collider2D player)
    {
        yield return new WaitForSeconds(2f); // ���� 2 �������

        CharacterStats characterStats = player.GetComponent<CharacterStats>();
        PlayerGold playerGold = player.GetComponent<PlayerGold>();

        if (characterStats != null && playerGold != null)
        {
            // ���������� ���� ������ � ������, �������� �� 10 (��������� �� ������ �����)
            int goldFromPower = characterStats.strength / 5;
            if (goldFromPower <= 0) { goldFromPower = 1; }

            // ��������� ���������� ������ ������
            playerGold.AddGold(goldFromPower);

            // �������� ���� ������
            //characterStats.SetPower(0);

            // ���������� ����� � ����������� ������
            if (goldText != null)
            {
                goldText.text = goldFromPower.ToString();
                goldText.gameObject.SetActive(true);
            }

            //Debug.Log("Player's power converted into " + goldFromPower + " gold.");
        }
    }
}
