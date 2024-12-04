using System.Collections;
using UnityEngine;
using System.Linq; // Для использования LINQ методов

public class BackgroundManager : MonoBehaviour
{
    // Список всех фонов (спрайтов)
    public Sprite[] backgrounds;

    // Ссылка на SpriteRenderer, который отображает текущий фон
    private SpriteRenderer backgroundRenderer;

    // Время задержки перед активацией
    //public float activationDelay = 2f;

    BackgroundScaler backgroundScaler;

    void Start()
    {
        backgroundScaler = GetComponent<BackgroundScaler>();
        // Получаем компонент SpriteRenderer на объекте
        backgroundRenderer = GetComponent<SpriteRenderer>();

        // Очищаем список от null-элементов
        CleanBackgroundList();
    }

    // Метод для очистки списка от null-элементов
    void CleanBackgroundList()
    {
        // Фильтруем массив, оставляя только непустые спрайты
        backgrounds = backgrounds.Where(sprite => sprite != null).ToArray();

        if (backgrounds.Length == 0)
        {
            Debug.LogWarning("No valid backgrounds available after cleaning!");
        }
    }

    // Метод для смены текущего фона на случайный
    public void SetRandomBackground()
    {
        if (backgrounds.Length == 0)
        {
            Debug.LogWarning("No backgrounds available!");
            return;
        }

        // Выбираем случайный фон
        int randomIndex = Random.Range(0, backgrounds.Length);

        // Устанавливаем случайный фон
        backgroundRenderer.sprite = backgrounds[randomIndex];
        backgroundScaler.ScaleBackground();
    }
}
