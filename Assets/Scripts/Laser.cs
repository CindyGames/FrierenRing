using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserDuration = 2f; // Время существования лазера
    public int laserDamage = 10; // Урон, который наносит лазер
    public Vector3 laserStartScale = new Vector3(1f, 0f, 1f); // Начальные размеры лазера (нуль по Y)
    public Vector3 laserEndScale = new Vector3(1f, 5f, 1f); // Конечные размеры лазера
    public float growSpeed = 5f; // Скорость роста лазера
    public float damageInterval = 0.5f; // Интервал между нанесением урона
    private SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer
    private bool isPlayerInLaser = false; // Флаг, что игрок в зоне лазера
    private CharacterStats playerStats; // Ссылка на компонент игрока

    void Start()
    {
        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Устанавливаем лазеру начальный размер (нуль по Y)
        transform.localScale = laserStartScale;

        // Запускаем таймер для уничтожения лазера через несколько секунд
        Invoke("DestroyLaser", laserDuration);
    }

    void Update()
    {
        // Постепенно увеличиваем высоту лазера до нужного значения
        if (transform.localScale.y < laserEndScale.y)
        {
            float newScaleY = Mathf.MoveTowards(transform.localScale.y, laserEndScale.y, growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(laserEndScale.x, newScaleY, laserEndScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Получаем скрипт здоровья игрока
            playerStats = other.GetComponent<CharacterStats>();

            if (playerStats != null && !isPlayerInLaser)
            {
                // Устанавливаем флаг, что игрок в зоне действия лазера
                isPlayerInLaser = true;

                // Запускаем корутину для периодического нанесения урона
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Когда игрок покидает зону действия лазера, останавливаем нанесение урона
            if (playerStats != null)
            {
                isPlayerInLaser = false;
                StopCoroutine(DealDamageOverTime());
            }
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (isPlayerInLaser)
        {
            if (playerStats != null)
            {
                // Наносим урон игроку
                playerStats.DecreasePower(laserDamage);
            }

            // Ждем интервал перед следующим нанесением урона
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Уничтожаем лазер через время
    private void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
