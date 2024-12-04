using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float minTimer = 2f; // Минимальное время перед взрывом
    public float maxTimer = 5f; // Максимальное время перед взрывом
    public float fallSpeed = 5f; // Скорость падения
    public float explosionRadius = 3f; // Радиус взрыва
    public int damage = 5; // Урон, который будет нанесен игроку
    public float pushForce = 10f; // Сила отталкивания

    private float timer; // Таймер
    private bool isFalling = true; // Флаг, указывающий, падает ли бомба
    private Animator animator; // Аниматор для взрыва

    void Start()
    {
        // Устанавливаем таймер в случайном диапазоне
        timer = Random.Range(minTimer, maxTimer);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Если бомба падает, перемещаем её вниз
        if (isFalling)
        {
            transform.Translate(Vector3.right * fallSpeed * Time.deltaTime); // Исправлено на падение вниз

            // Уменьшаем таймер
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        isFalling = false; // Останавливаем падение
        animator.SetTrigger("Explode"); // Воспроизводим анимацию взрыва
        DealDamageToPlayersInRange();
        Destroy(gameObject, 1f); // Уничтожаем бомбу через 1 секунду после взрыва
    }

    public void DealDamageToPlayersInRange()
    {
        // Находим всех игроков в радиусе взрыва
        Collider2D[] playersHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in playersHit)
        {
            if (hit.CompareTag("Player"))
            {
                // Получаем компонент игрока и наносим урон
                CharacterStats playerHealth = hit.GetComponent<CharacterStats>();
                if (playerHealth != null)
                {
                    playerHealth.DecreasePower(damage);
                }

                // Рассчитываем направление отталкивания
                Vector2 pushDirection = Vector2.up; // Направление отталкивания только вверх
                Rigidbody2D playerRigidbody = hit.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // Применяем силу
                }
            }
        }
    }

    // Для отрисовки радиуса взрыва в редакторе
    private void OnDrawGizmosSelected()
    {
        // Устанавливаем цвет для Gizmos
        Gizmos.color = Color.red;
        // Рисуем окружность радиуса взрыва
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
