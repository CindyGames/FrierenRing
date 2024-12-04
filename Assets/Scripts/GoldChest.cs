using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldChest : MonoBehaviour
{
    public int minGoldAmount = 50; // Минимальное количество золота
    public int maxGoldAmount = 150; // Максимальное количество золота
    public TextMeshProUGUI goldText; // Текст для отображения полученного золота

    private bool isOpened = false; // Проверка, открыт ли сундук
    private int goldAmount; // Случайное количество золота, которое сундук даёт игроку
    private Chest chest;
    void Start()
    {
        chest = GetComponent<Chest>();
        if (goldText != null)
        {
            goldText.gameObject.SetActive(false); // Скрываем текст с золотом в начале
        }

        // Генерация случайного количества золота в диапазоне от minGoldAmount до maxGoldAmount
        goldAmount = Random.Range(minGoldAmount, maxGoldAmount + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если игрок касается сундука
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true; // Отмечаем, что сундук был открыт
            StartCoroutine(OpenChestAfterDelay(other));
            other.GetComponent<MobileCharacterController>().stopMove = true;

        }
    }

    // Сопрограмма для открытия сундука и начисления золота через 2 секунды
    IEnumerator OpenChestAfterDelay(Collider2D player)
    {
        chest.IsOpened = true;

        yield return new WaitForSeconds(2f); // Ждем 2 секунды


        player.GetComponent<PlayerGold>().AddGold(goldAmount);


        // Активируем текст с золотом
        if (goldText != null)
        {
            goldText.text = goldAmount.ToString();
            goldText.gameObject.SetActive(true);
        }

        //Debug.Log("Player received " + goldAmount + " gold.");
    }
}
