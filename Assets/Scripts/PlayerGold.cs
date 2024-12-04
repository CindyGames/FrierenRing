using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int gold = 0; // Количество золота
    public TextMeshProUGUI goldText; // Текстовое поле для отображения количества золота

    public AudioSource audioSource;

    private const string GoldKey = "PlayerGold"; // Ключ для хранения золота в PlayerPrefs

    void Start()
    {
        // Загружаем количество золота при старте уровня
        LoadGold();
        UpdateGoldText();
    }

    // Метод для увеличения количества золота
    public void AddGold(int amount)
    {
        PlaySound(); // Метод для воспроизведения звука

        gold += amount;
        UpdateGoldText();
        SaveGold(); // Сохраняем золото после изменения
                    

    }

    // Метод для обновления текста с количеством золота
    void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString();
        }
    }

    // Метод для сохранения количества золота
    void SaveGold()
    {
        PlayerPrefs.SetInt(GoldKey, gold);
        PlayerPrefs.Save();
    }

    // Метод для загрузки сохраненного количества золота
    void LoadGold()
    {
        if (PlayerPrefs.HasKey(GoldKey))
        {
            gold = PlayerPrefs.GetInt(GoldKey);
        }
    }

    // Метод для воспроизведения звука
    public void PlaySound()
    {
        if (audioSource != null)
        {
            // Воспроизведение звука
            audioSource.Play();
        }
    }
}
