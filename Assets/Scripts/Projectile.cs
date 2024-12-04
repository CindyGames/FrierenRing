using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10; // Начальная скорость снаряда
    public bool DestroyOnHit = true;

    private Vector2 direction; // Направление полета снаряда
    private InitProjectile init;
    private int damage; // Урон снаряда


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
        // Перемещаем снаряд в заданном направлении
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage); // Добавляем врага в список
                DestroyProjectile();
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null)
            {
                bossStats.TakeDamage(damage); // Добавляем босса в список
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
