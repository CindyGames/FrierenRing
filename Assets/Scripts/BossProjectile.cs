using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;           // Скорость снаряда
    public int damageAmount = 1;       // Урон, который наносит снаряд

    void Update()
    {
        // Снаряд движется вниз с постоянной скоростью
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // Метод для обработки столкновения с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                // Уменьшаем мощь игрока при столкновении
                characterStats.DecreasePower(damageAmount);
            }

            // Уничтожаем снаряд после столкновения с игроком
            Destroy(gameObject);
        }
    }

    // Опционально: Если снаряд выходит за пределы экрана, можно его уничтожить, чтобы не нагружать систему
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
