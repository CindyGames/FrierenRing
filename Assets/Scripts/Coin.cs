using UnityEngine;

public class Coin : MonoBehaviour
{
    public int goldAmount = 1;  // Сколько золота даёт монета

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если игрок коснулся монеты
        if (other.CompareTag("Player"))
        {
            // Получаем компонент PlayerGold у игрока
            PlayerGold playerGold = other.GetComponent<PlayerGold>();

            if (playerGold != null)
            {
                // Добавляем золото игроку
                playerGold.AddGold(goldAmount);

                // Уничтожаем монету
                Destroy(gameObject);
            }
        }
    }
}
