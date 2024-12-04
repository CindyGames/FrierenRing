using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float speed = 5f; // �������� �������� ������� ����
    public GameObject iceDebuffPrefab; // ������ ������ ����

    private bool hasHitPlayer = false; // ���� ��� �������� ������������ � �������

    private void Update()
    {
        // �������� ������� ���� �� ������� ������������
        if (!hasHitPlayer)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���������� �� ������ � �������
        if (collision.CompareTag("Player"))
        {
            // ������� ����� ��� ������������ � �������
            ApplyDebuff(collision.gameObject);

            // ������������� ������ � ���������� ���
            hasHitPlayer = true;
            Destroy(gameObject);
        }
    }

    // ����� ��� ���������� ������ � ������
    private void ApplyDebuff(GameObject player)
    {
        // ������� ����� �� ������� � ����������� ��� � ������
        GameObject debuff = Instantiate(iceDebuffPrefab, player.transform.position, Quaternion.identity);
        debuff.transform.SetParent(player.transform); // ���������� ����� �� �������
    }
}
