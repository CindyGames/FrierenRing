using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10; // ��������� �������� �������
    public bool DestroyOnHit = true;

    private Vector2 direction; // ����������� ������ �������
    private InitProjectile init;
    private int damage; // ���� �������


    private void Start()
    {
        init = GetComponent<InitProjectile>();
        damage = init.damage;
        direction = init.direction.normalized;
        speed += init.moveSpead;
        Destroy(gameObject, 2f);
    }


    void Update()
    {
        // ���������� ������ � �������� �����������
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
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
        if (DestroyOnHit)
        {
            Destroy(gameObject);

        }
    }
}
