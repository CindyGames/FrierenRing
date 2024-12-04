using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;           // �������� �������
    public int damageAmount = 1;       // ����, ������� ������� ������

    void Update()
    {
        // ������ �������� ���� � ���������� ���������
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // ����� ��� ��������� ������������ � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                // ��������� ���� ������ ��� ������������
                characterStats.DecreasePower(damageAmount);
            }

            // ���������� ������ ����� ������������ � �������
            Destroy(gameObject);
        }
    }

    // �����������: ���� ������ ������� �� ������� ������, ����� ��� ����������, ����� �� ��������� �������
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
