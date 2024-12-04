using TMPro;
using UnityEngine;

public class PowerBonus : MonoBehaviour
{
    public TextMeshProUGUI bonusText; // Ссылка на UI элемент для отображения бонуса
    public int minAddValue = 5; // Минимальное значение добавления
    public int maxAddValue = 15; // Максимальное значение добавления
    public int minMultiplier = 1; // Минимальный множитель
    public int maxMultiplier = 2; // Максимальный множитель
    [Range(0f, 1f)] public float additionChance = 0.7f; // Вероятность выпадения добавления (0.7 = 70%)
    private int bonusValue; // Хранит значение бонуса
    private bool isAddition; // Определяет тип бонуса (true - добавление, false - умножение)

    private void Start()
    {
        bonusText = GetComponent<TextMeshProUGUI>();
        // Случайный выбор между добавлением или умножением на основе вероятности
        isAddition = Random.value < additionChance;

        if (isAddition)
        {
            // Случайное значение для добавления
            bonusValue = Random.Range(minAddValue, maxAddValue);
            bonusText.text = "+" + bonusValue.ToString();
        }
        else
        {
            // Случайное значение для умножения
            bonusValue = Random.Range(minMultiplier, maxMultiplier);
            bonusText.text = "x" + bonusValue.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                ApplyBonus(characterStats);
            }

            // Удаляем бонус после использования
            Destroy(gameObject);
        }
    }

    private void ApplyBonus(CharacterStats characterStats)
    {
        if (isAddition)
        {
            // Добавляем случайное число к мощности
            characterStats.IncreasePower(bonusValue);
        }
        else
        {
            // Умножаем мощность на случайное число
            characterStats.MultiplyPower(bonusValue);
        }
    }
}
