using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f; // ������� �������� �������
    private Transform target; // ����, �� ������� ����� ��������� ������
    public int damage = 1;

    Vector2 shootPosition;

    private void Start()
    {
        shootPosition = transform.position;
        target = FindNearestEnemyAbove();
        Destroy(gameObject, 10f);

    }

    // ������������� ���� �������
    public void Initialize(int dmg, float velociti)
    {
        damage = dmg;
        speed += velociti;
    }

    void Update()
    {
        if (target == null)
        {
            target = FindNearestEnemyAbove();
            return;
        }

        // ��������� ���������� �� ����
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            Destroy(gameObject);
            return;
        }

        // ��������� ����������� � ����
        Vector2 direction = (target.position - transform.position).normalized;

        // ���������� ������ � ������� ����
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // ������������ ������ � ������� ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ����� ��� ������ ���������� �����, ������������ ���� ����� ��������
    private Transform FindNearestEnemyAbove()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.y > shootPosition.y) // ��������, ��� ���� ���� ����� ��������
            {
                float distance = Vector2.Distance(shootPosition, enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }

        if (nearestEnemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        return nearestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
                DestroyProjectile();
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null)
            {
                bossStats.TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
