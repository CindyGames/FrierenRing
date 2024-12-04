using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float speed = 5f; // Скорость движения снаряда вниз
    public GameObject iceDebuffPrefab; // Префаб дебафа льда

    private bool hasHitPlayer = false; // Флаг для проверки столкновения с игроком

    private void Update()
    {
        // Движение снаряда вниз до момента столкновения
        if (!hasHitPlayer)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, столкнулся ли снаряд с игроком
        if (collision.CompareTag("Player"))
        {
            // Создаем дебаф при столкновении с игроком
            ApplyDebuff(collision.gameObject);

            // Останавливаем снаряд и уничтожаем его
            hasHitPlayer = true;
            Destroy(gameObject);
        }
    }

    // Метод для применения дебафа к игроку
    private void ApplyDebuff(GameObject player)
    {
        // Создаем дебаф из префаба и привязываем его к игроку
        GameObject debuff = Instantiate(iceDebuffPrefab, player.transform.position, Quaternion.identity);
        debuff.transform.SetParent(player.transform); // Закрепляем дебаф за игроком
    }
}
