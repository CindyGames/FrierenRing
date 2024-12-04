using System.Collections;
using TMPro;
using UnityEngine;

public class PowerToGoldStatue : MonoBehaviour
{
    public TextMeshProUGUI goldText; // Текст для отображения полученного золота

    private bool isActivated = false; // Проверка, активировалась ли статуя

    void Start()
    {
        if (goldText != null)
        {
            goldText.gameObject.SetActive(false); // Скрываем текст с золотом в начале
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если игрок касается статуи
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Отмечаем, что статуя была активирована
            StartCoroutine(ActivateStatueAfterDelay(other));
            other.GetComponent<MobileCharacterController>().stopMove = true;
        }
    }

    // Сопрограмма для активации статуи и превращения силы в золото через 2 секунды
    IEnumerator ActivateStatueAfterDelay(Collider2D player)
    {
        yield return new WaitForSeconds(2f); // Ждем 2 секунды

        CharacterStats characterStats = player.GetComponent<CharacterStats>();
        PlayerGold playerGold = player.GetComponent<PlayerGold>();

        if (characterStats != null && playerGold != null)
        {
            // Превращаем мощь игрока в золото, деленное на 10 (округляем до целого числа)
            int goldFromPower = characterStats.strength / 5;
            if (goldFromPower <= 0) { goldFromPower = 1; }

            // Добавляем полученное золото игроку
            playerGold.AddGold(goldFromPower);

            // Обнуляем мощь игрока
            //characterStats.SetPower(0);

            // Активируем текст с количеством золота
            if (goldText != null)
            {
                goldText.text = goldFromPower.ToString();
                goldText.gameObject.SetActive(true);
            }

            //Debug.Log("Player's power converted into " + goldFromPower + " gold.");
        }
    }
}
