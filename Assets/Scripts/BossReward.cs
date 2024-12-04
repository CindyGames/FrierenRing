using TMPro;
using UnityEngine;

public class BossReward : MonoBehaviour
{
    public string bossID = "Boss1"; // Уникальный идентификатор босса, чтобы различать его от других боссов
    public int rewardAmount = 100; // Количество награды за победу над боссом
    private string victoryKey; // Ключ для проверки, побежден ли босс

    public TMP_Text textReward;

    void Start()
    {
        // Генерируем ключ для сохранения состояния победы над боссом
        victoryKey = bossID + "_Defeated";
    }

    // Метод, вызываемый при победе над боссом
    public void OnBossDefeated()
    {
        // Проверяем, побеждал ли игрок этого босса ранее
        if (!IsBossDefeated())
        {
            textReward.gameObject.SetActive(true);

            // Выдаём награду, если босс побеждён впервые
            GiveReward();
            MarkBossAsDefeated(); // Сохраняем состояние победы
        }
        else
        {
            Debug.Log("Босс уже побеждён. Награда не выдана.");
        }
    }

    // Проверяем, побеждал ли игрок босса
    bool IsBossDefeated()
    {
        return PlayerPrefs.GetInt(victoryKey, 0) == 1; // Возвращает 1, если босс уже побеждён, или 0, если нет
    }

    // Сохраняем состояние победы над боссом
    void MarkBossAsDefeated()
    {
        PlayerPrefs.SetInt(victoryKey, 1); // Помечаем босса как побеждённого
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    // Метод для выдачи награды игроку
    void GiveReward()
    {

        // Здесь можно реализовать логику выдачи награды, например, увеличить количество монет у игрока
        Debug.Log("Награда выдана: " + rewardAmount);
        // Пример: PlayerStats.Instance.AddCoins(rewardAmount);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>().AddGold(rewardAmount);
    }
}
