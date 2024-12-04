using UnityEngine;

public class ReturningProjectile : MonoBehaviour
{
    public float forwardSpeed = 10f; // �������� ����� �� ���������
    public float returnSpeed = 15f; // �������� ����������� � ���������
    public float returnDelay = 1f; // �������� ����� ������������
    public float rotationSpeed = 360f; // �������� �������� ������� (������� � �������)

    private int damage;
    private Transform player; // ��������� ���������, � �������� ������������ ������
    private Vector2 initialDirection; // �����������, � ������� ������ ��������
    private bool isReturning = false; // ����, �����������, ��� ������ ������������

    private InitProjectile init;

    private void Start()
    {
        init = GetComponent<InitProjectile>();
        damage = init.damage;
        initialDirection = init.direction.normalized;
        player = init.manager.transform;
        forwardSpeed += init.moveSpead;
        returnSpeed += init.moveSpead;

        Invoke(nameof(BeginReturn), returnDelay); // ������������� �������� ����� ���������

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        RotateProjectile(); // ������� ������ ������ ���

        if (!isReturning)
        {
            // ��������� � ��������� �����������
            transform.position += (Vector3)initialDirection * forwardSpeed * Time.deltaTime;
        }
        else
        {
            // ������������ � ������� ���������
            Vector2 returnDirection = (player.position - transform.position).normalized;
            transform.position += (Vector3)returnDirection * returnSpeed * Time.deltaTime;

            // ��������� �������� ����������� �� ����������� ��������
            // ���������� ������, ����� �� ��������� ���������
            if (Vector2.Distance(transform.position, player.position) < 0.1f)
            {
                Destroy(gameObject);
            }



        }
    }

    // ����� ��� �������� �������
    private void RotateProjectile()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // ������� ������ ������ ��� Z
    }

    // ����� ��� ������ �������� � ���������
    private void BeginReturn()
    {
        isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage); // ��������� ����� � ������
                DestroyProjectile();
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null)
            {
                bossStats.TakeDamage(damage); // ��������� ����� � ������
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
