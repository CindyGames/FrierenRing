using UnityEngine;

public class Coin : MonoBehaviour
{
    public int goldAmount = 1;  // ������� ������ ��� ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� ����� �������� ������
        if (other.CompareTag("Player"))
        {
            // �������� ��������� PlayerGold � ������
            PlayerGold playerGold = other.GetComponent<PlayerGold>();

            if (playerGold != null)
            {
                // ��������� ������ ������
                playerGold.AddGold(goldAmount);

                // ���������� ������
                Destroy(gameObject);
            }
        }
    }
}
