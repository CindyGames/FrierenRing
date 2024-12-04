using TMPro;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Для вычитания мощности
    public int minDamage = 1;  // Минимальный урон для вычитания
    public int maxDamage = 5;  // Максимальный урон для вычитания

    // Для деления мощности
    public int minDivideValue = 2; // Минимальное значение для деления
    public int maxDivideValue = 4; // Максимальное значение для деления

    public TextMeshProUGUI trapEffectText;  // Текстовый элемент для отображения эффекта (опционально)
    public bool canDividePower = true; // Определяет, может ли ловушка делить мощь

    private int trapEffectValue;
    private Animator trapAnimator;
    private bool willDividePower; // Флаг, будет ли ловушка делить мощь

    private void Start()
    {
        trapAnimator = GetComponent<Animator>();
        if (trapEffectText == null)
        {
            trapEffectText = GetComponentInChildren<TextMeshProUGUI>();
        }

        DetermineEffect(); // Определяем, что будет делать ловушка: отнимать или делить мощь
        GenerateEffectValue();
        DisplayTrapEffect();
    }

    // Метод для определения эффекта ловушки: отнять или разделить мощь
    private void DetermineEffect()
    {
        if (canDividePower)
        {
            // Случайно выбираем, будет ли ловушка отнимать или делить мощь
            willDividePower = (Random.value > 0.5f);
        }
        else
        {
            willDividePower = false; // Если деление отключено, всегда отнимаем
        }
    }

    // Метод для генерации случайного значения эффекта
    private void GenerateEffectValue()
    {
        if (willDividePower)
        {
            // Генерируем случайное значение для деления
            trapEffectValue = Random.Range(minDivideValue, maxDivideValue + 1);
        }
        else
        {
            // Генерируем случайное значение для вычитания
            trapEffectValue = Random.Range(minDamage, maxDamage + 1);
        }
    }

    // Отображаем эффект ловушки над ней (опционально)
    private void DisplayTrapEffect()
    {
        if (trapEffectText != null)
        {
            if (willDividePower)
            {
                trapEffectText.text = "÷" + trapEffectValue.ToString(); // Отображаем деление
            }
            else
            {
                trapEffectText.text = "-" + trapEffectValue.ToString(); // Отображаем отнимание
            }
        }
    }

    // Когда герой наступает на ловушку
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                ApplyTrapEffect(characterStats);
            }
            if (trapAnimator != null)
            {
                trapAnimator.SetTrigger("start");
            }

            trapEffectText.gameObject.SetActive(false);
        }
    }

    // Применяем эффект ловушки к персонажу
    private void ApplyTrapEffect(CharacterStats characterStats)
    {
        if (willDividePower)
        {
            // Если ловушка делит мощь, уменьшаем её путём деления
            characterStats.DividePower(trapEffectValue);
            Debug.Log("Trap divided hero's power by " + trapEffectValue);
        }
        else
        {
            // Если ловушка отнимает мощь
            characterStats.DecreasePower(trapEffectValue);
            Debug.Log("Trap dealt " + trapEffectValue + " damage to the hero.");
        }
    }
}
