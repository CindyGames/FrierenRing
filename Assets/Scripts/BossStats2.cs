using System.Collections;
using TMPro;
using UnityEngine;

public class BossStats2 : MonoBehaviour
{
    [SerializeField]
    int minHealth, maxHealth;  // Диапазон здоровья босса

    private int health; // Текущее здоровье босса


    public TextMeshProUGUI healthText; // Текстовый UI элемент для отображения здоровья
    public GameObject victoryScreen; // Экран победы

    private bool raige = false;
    public AudioSource bossMusic; // Ссылка на AudioSource для фоновой музыки
    public AudioSource hitMusic; // Ссылка на AudioSource для фоновой музыки

    private void Start()
    {
        if (victoryScreen == null)
        {
            victoryScreen = GameObject.FindGameObjectWithTag("Win Screen").GetComponent<WinActivate>().winScreen;
        }

        victoryScreen.SetActive(false);

        //bossMusic = GetComponent<AudioSource>();

        healthText = GetComponentInChildren<TextMeshProUGUI>();
        // Задаем случайное начальное здоровье в указанном диапазоне
        health = Random.Range(minHealth, maxHealth);
        UpdateHealthUI();
    }

    // Обновляем UI с текущим здоровьем босса
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    // Метод для получения урона от снарядов
    public void TakeDamage(int damageAmount)
    {
        hitMusic.Play();

        health -= damageAmount;
        UpdateHealthUI();
        if (health < maxHealth / 2 && raige == false)
        {
            raige = true;
            GetComponent<BossAttack>().attackInterval /= 2;
        }
        // Если здоровье достигло 0 или ниже, босс погибает
        if (health <= 0)
        {
            DefeatBoss();
        }
    }



    // Метод для победы над боссом
    private void DefeatBoss()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MobileCharacterController>().stopMove = true;
        //player.GetComponent<PlayerShooting>().stopShoot = true;
        //player.GetComponent<Collider2D>().enabled = false;

        //GetComponent<BossReward>().OnBossDefeated();

        // Активируем экран победы
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

        GetComponent<BossAttack>().attackInterval = 99; //stop attack

        if (bossMusic != null)
        {
            StartCoroutine(FadeOutMusic());
        }
        // Уничтожаем объект босса
        Destroy(gameObject, 4f);
    }


    // Сопрограмма для плавного снижения громкости музыки
    private IEnumerator FadeOutMusic()
    {
        float startVolume = bossMusic.volume;
        float fadeDuration = 4f; // Продолжительность затухания музыки

        // Плавное уменьшение громкости
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bossMusic.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Устанавливаем громкость на 0 и останавливаем музыку
        bossMusic.volume = 0;
        bossMusic.Stop();
    }
}
