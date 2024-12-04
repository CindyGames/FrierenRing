using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float minTimer = 2f; // ����������� ����� ����� �������
    public float maxTimer = 5f; // ������������ ����� ����� �������
    public float fallSpeed = 5f; // �������� �������
    public float explosionRadius = 3f; // ������ ������
    public int damage = 5; // ����, ������� ����� ������� ������
    public float pushForce = 10f; // ���� ������������

    private float timer; // ������
    private bool isFalling = true; // ����, �����������, ������ �� �����
    private Animator animator; // �������� ��� ������

    void Start()
    {
        // ������������� ������ � ��������� ���������
        timer = Random.Range(minTimer, maxTimer);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ���� ����� ������, ���������� � ����
        if (isFalling)
        {
            transform.Translate(Vector3.right * fallSpeed * Time.deltaTime); // ���������� �� ������� ����

            // ��������� ������
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        isFalling = false; // ������������� �������
        animator.SetTrigger("Explode"); // ������������� �������� ������
        DealDamageToPlayersInRange();
        Destroy(gameObject, 1f); // ���������� ����� ����� 1 ������� ����� ������
    }

    public void DealDamageToPlayersInRange()
    {
        // ������� ���� ������� � ������� ������
        Collider2D[] playersHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in playersHit)
        {
            if (hit.CompareTag("Player"))
            {
                // �������� ��������� ������ � ������� ����
                CharacterStats playerHealth = hit.GetComponent<CharacterStats>();
                if (playerHealth != null)
                {
                    playerHealth.DecreasePower(damage);
                }

                // ������������ ����������� ������������
                Vector2 pushDirection = Vector2.up; // ����������� ������������ ������ �����
                Rigidbody2D playerRigidbody = hit.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // ��������� ����
                }
            }
        }
    }

    // ��� ��������� ������� ������ � ���������
    private void OnDrawGizmosSelected()
    {
        // ������������� ���� ��� Gizmos
        Gizmos.color = Color.red;
        // ������ ���������� ������� ������
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
