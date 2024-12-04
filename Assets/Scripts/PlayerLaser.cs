using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public float laserDuration = 2f; // Время существования лазера
    private int laserDamage = 1; // Урон, который наносит лазер

    public Vector3 laserStartScale = new Vector3(1f, 0f, 1f); // Начальные размеры лазера (нуль по Y)
    public Vector3 laserEndScale = new Vector3(1f, 5f, 1f); // Конечные размеры лазера
    public float growSpeed = 5f; // Скорость роста лазера
    public float damageInterval = 0.5f; // Интервал между нанесением урона
    private SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer

    private List<EnemyStats> enemiesInLaser = new List<EnemyStats>(); // Список врагов в зоне лазера
    private List<BossStats2> bossesInLaser = new List<BossStats2>(); // Список боссов в зоне лазера

    public void Initialize(int dmg, float damageInter)
    {
        laserDamage = dmg;
        damageInterval = damageInter;
    }

    void Start()
    {

        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Устанавливаем лазеру начальный размер (нуль по Y)
        transform.localScale = laserStartScale;

        // Запускаем таймер для уничтожения лазера через несколько секунд
        Invoke("DestroyLaser", laserDuration);

        // Запускаем корутину для периодического нанесения урона
        StartCoroutine(DealDamageOverTime());
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
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null && !enemiesInLaser.Contains(enemyStats))
            {
                enemiesInLaser.Add(enemyStats); // Добавляем врага в список
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null && !bossesInLaser.Contains(bossStats))
            {
                bossesInLaser.Add(bossStats); // Добавляем босса в список
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemiesInLaser.Remove(enemyStats); // Убираем врага из списка
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null)
            {
                bossesInLaser.Remove(bossStats); // Убираем босса из списка
            }
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            // Наносим урон всем врагам в зоне
            for (int i = enemiesInLaser.Count - 1; i >= 0; i--)
            {
                if (enemiesInLaser[i] != null)
                {
                    enemiesInLaser[i].TakeDamage(laserDamage);
                }
                else
                {
                    enemiesInLaser.RemoveAt(i);
                }
            }

            // Наносим урон всем боссам в зоне
            for (int i = bossesInLaser.Count - 1; i >= 0; i--)
            {
                if (bossesInLaser[i] != null)
                {
                    bossesInLaser[i].TakeDamage(laserDamage);
                }
                else
                {
                    bossesInLaser.RemoveAt(i);
                }
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
