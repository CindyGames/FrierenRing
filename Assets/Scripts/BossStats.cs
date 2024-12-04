using System.Collections;
using TMPro;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    [SerializeField]
    int minAddValue, maxAddValue;

    private int power; // Сила врага
    public TextMeshProUGUI powerText; // Текстовый UI элемент для отображения силы
    public GameObject defeatEffect; // Эффект поражения (например, анимация смерти)

    public GameObject wictoryScreen;

    private void Start()
    {
        powerText = GetComponentInChildren<TextMeshProUGUI>();
        // Случайное значение для добавления
        power = Random.Range(minAddValue, maxAddValue);
        UpdatePowerUI();
    }

    // Обновляем UI с текущей силой врага
    private void UpdatePowerUI()
    {
        powerText.text = power.ToString();
    }

    // Метод для установки новой силы (например, когда герой получает силу врага)
    public void SetPower(int newPower)
    {
        power = newPower;
        UpdatePowerUI();
    }

    // Взаимодействие с героем
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                // Запускаем сопрограмму, которая выполнит метод через 2 секунды
                StartCoroutine(InvokeMethodAfterDelay(characterStats));

            }

            other.GetComponent<MobileCharacterController>().stopMove = true;


        }
    }

    // Разрешение битвы: герой или враг побеждает
    private void ResolveFight(CharacterStats characterStats)
    {
        if (characterStats.strength >= power)
        {
            // Герой побеждает, получает силу врага
            //characterStats.IncreasePower(power);
            DefeatEnemy();
        }
        else if (characterStats.strength < power)
        {
            // Герой проигрывает
            //characterStats.Die();
        }
    }

    // Метод для поражения врага
    private void DefeatEnemy()
    {
        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity);
        }
        powerText.text = "0";
        //Destroy(gameObject); // Удаляем врага из игры

        if (wictoryScreen != null)
        {
            wictoryScreen.SetActive(true);
        }

    }



    // Сопрограмма, которая ждёт 2 секунды перед вызовом метода
    IEnumerator InvokeMethodAfterDelay(CharacterStats characterStats)
    {
        yield return new WaitForSeconds(2f); // Ждём 2 секунды
        ResolveFight(characterStats); // Вызов метода после задержки
    }


}
