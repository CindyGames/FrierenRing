using UnityEngine;

public class ReturningProjectile : MonoBehaviour
{
    public float forwardSpeed = 10f; // Скорость полёта от персонажа
    public float returnSpeed = 15f; // Скорость возвращения к персонажу
    public float returnDelay = 1f; // Задержка перед возвращением
    public float rotationSpeed = 360f; // Скорость вращения снаряда (градусы в секунду)

    private int damage;
    private Transform player; // Трансформ персонажа, к которому возвращается снаряд
    private Vector2 initialDirection; // Направление, в котором снаряд вылетает
    private bool isReturning = false; // Флаг, указывающий, что снаряд возвращается

    private InitProjectile init;

    private void Start()
    {
        init = GetComponent<InitProjectile>();
        damage = init.damage;
        initialDirection = init.direction.normalized;
        player = init.manager.transform;
        forwardSpeed += init.moveSpead;
        returnSpeed += init.moveSpead;

        Invoke(nameof(BeginReturn), returnDelay); // Устанавливаем задержку перед возвратом

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        RotateProjectile(); // Вращаем снаряд вокруг оси

        if (!isReturning)
        {
            // Двигаемся в начальном направлении
            transform.position += (Vector3)initialDirection * forwardSpeed * Time.deltaTime;
        }
        else
        {
            // Возвращаемся к позиции персонажа
            Vector2 returnDirection = (player.position - transform.position).normalized;
            transform.position += (Vector3)returnDirection * returnSpeed * Time.deltaTime;

            // Оставляем вращение независимым от направления движения
            // Уничтожаем снаряд, когда он достигает персонажа
            if (Vector2.Distance(transform.position, player.position) < 0.1f)
            {
                Destroy(gameObject);
            }



        }
    }

    // Метод для вращения снаряда
    private void RotateProjectile()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Вращаем снаряд вокруг оси Z
    }

    // Метод для начала возврата к персонажу
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
        Destroy(gameObject);
    }
}
