using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f; // Базовая скорость снаряда
    private Transform target; // Цель, за которой будет следовать снаряд
    public int damage = 1;

    Vector2 shootPosition;

    private void Start()
    {
        shootPosition = transform.position;
        target = FindNearestEnemyAbove();
        Destroy(gameObject, 10f);

    }

    // Инициализация цели снаряда
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

        // Проверяем расстояние до цели
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            Destroy(gameObject);
            return;
        }

        // Вычисляем направление к цели
        Vector2 direction = (target.position - transform.position).normalized;

        // Перемещаем снаряд в сторону цели
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Поворачиваем снаряд в сторону движения
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Метод для поиска ближайшего врага, находящегося выше точки выстрела
    private Transform FindNearestEnemyAbove()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.y > shootPosition.y) // Проверка, что враг выше точки выстрела
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
