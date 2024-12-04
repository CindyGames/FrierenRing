using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Для работы с TextMeshPro

public class LevelUpButton : MonoBehaviour
{
    public Button levelUpButton;          // Ссылка на компонент кнопки
    public CharacterStats characterStats; // Ссылка на компонент CharacterStats персонажа
    public PlayerGold playerGold;         // Ссылка на компонент PlayerGold для проверки золота
    public int baseIncreaseAmount = 1;    // Базовое количество, на которое увеличиваем силу
    public int baseUpgradeCost = 10;      // Базовая стоимость повышения уровня
    private int currentUpgradeCost;       // Текущая стоимость улучшения
    private int currentIncreaseAmount;    // Текущее количество увеличения силы
    public float costMultiplier = 1.5f;   // Множитель для увеличения стоимости улучшения
    public float powerMultiplier = 1.2f;  // Множитель для увеличения силы

    public TextMeshProUGUI upgradeCostText;  // Ссылка на TextMeshProUGUI для отображения стоимости
    public TextMeshProUGUI powerIncreaseText; // Ссылка на TextMeshProUGUI для отображения увеличения силы

    private const string PowerKey = "PlayerPower";           // Ключ для сохранения силы игрока
    private const string UpgradeCostKey = "UpgradeCost";     // Ключ для сохранения стоимости улучшения
    private const string IncreaseAmountKey = "IncreaseAmount"; // Ключ для сохранения количества увеличения силы

    void Start()
    {
        if (levelUpButton == null)
        {
            levelUpButton = GetComponent<Button>();
        }

        // Загружаем сохраненные данные
        LoadData();

        // Применяем сохраненную силу к персонажу
        if (characterStats != null)
        {
            characterStats.SetPower(PlayerPrefs.GetInt(PowerKey, baseIncreaseAmount));
        }

        // Обновляем текст стоимости улучшения и силы
        UpdateUpgradeCostText();

        // Подписываем метод на событие нажатия кнопки
        levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
    }

    // Метод, который вызывается при нажатии на кнопку
    void OnLevelUpButtonClick()
    {
        // Проверяем, достаточно ли золота для повышения уровня
        if (playerGold != null && playerGold.gold >= currentUpgradeCost)
        {
            // Уменьшаем количество золота на стоимость улучшения
            playerGold.AddGold(-currentUpgradeCost);

            // Повышаем силу персонажа
            if (characterStats != null)
            {
                characterStats.IncreasePower(currentIncreaseAmount);
                // Сохраняем новую силу персонажа
                PlayerPrefs.SetInt(PowerKey, characterStats.strength);
            }

            // Увеличиваем стоимость следующего уровня
            currentUpgradeCost = Mathf.CeilToInt(currentUpgradeCost * costMultiplier);
            PlayerPrefs.SetInt(UpgradeCostKey, currentUpgradeCost); // Сохраняем стоимость улучшения

            // Увеличиваем количество силы для следующего уровня
            currentIncreaseAmount = Mathf.CeilToInt(currentIncreaseAmount * powerMultiplier);
            PlayerPrefs.SetInt(IncreaseAmountKey, currentIncreaseAmount); // Сохраняем количество увеличения силы

            // Обновляем текст стоимости улучшения и увеличения силы
            UpdateUpgradeCostText();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade.");
        }
    }

    // Метод для обновления текста стоимости улучшения и количества силы
    void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            upgradeCostText.text = currentUpgradeCost.ToString();
        }

        if (powerIncreaseText != null)
        {
            powerIncreaseText.text = "+ " + currentIncreaseAmount.ToString();
        }
    }

    // Метод для загрузки сохраненных данных
    void LoadData()
    {
        // Загружаем стоимость улучшения, если она была сохранена
        currentUpgradeCost = PlayerPrefs.GetInt(UpgradeCostKey, baseUpgradeCost);

        // Загружаем количество увеличения силы, если оно было сохранено
        currentIncreaseAmount = PlayerPrefs.GetInt(IncreaseAmountKey, baseIncreaseAmount);
    }
}
