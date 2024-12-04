using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backgroundPrefab; // Префаб фона для создания копий

    private float backgroundHeight; // Высота одного фона
    private Transform playerTransform; // Ссылка на игрока или камеру
    private GameObject[] backgrounds; // Массив для хранения фонов
    private int currentBackgroundIndex = 0; // Индекс текущего фона

    void Start()
    {
        // Проверяем наличие префаба фона
        if (backgroundPrefab == null)
        {
            Debug.LogError("backgroundPrefab не назначен в инспекторе.");
            return; // Прерываем выполнение, если префаб не назначен
        }

        // Получаем ссылку на игрока (или камеру)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Игрок с тегом 'Player' не найден.");
            return; // Прерываем выполнение, если игрок не найден
        }

        // Определяем высоту фона автоматически
        backgroundHeight = GetBackgroundHeight();
        if (backgroundHeight == 0f)
        {
            Debug.LogError("Не удалось определить высоту фона. Проверьте наличие SpriteRenderer на backgroundPrefab.");
            return; // Прерываем выполнение, если высота не определена
        }

        // Создаем массив фонов и размещаем их
        backgrounds = new GameObject[4]; // Будет четыре фона: основной и три копии
        backgrounds[0] = this.gameObject; // Текущий фон

        // Создаем три копии фона
        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgroundPrefab != null)
            {
                // Создаем копию префаба
                GameObject newBackground = Instantiate(backgroundPrefab, transform.position + new Vector3(0, backgroundHeight * i, 0), Quaternion.identity);

                // Удаляем скрипт InfiniteBackground с копии
                InfiniteBackground script = newBackground.GetComponent<InfiniteBackground>();
                if (script != null)
                {
                    Destroy(script);
                }

                // Добавляем копию в массив фонов
                backgrounds[i] = newBackground;
            }
        }


    }

    void Update()
    {
        if (playerTransform == null) return; // Проверка на наличие игрока

        // Проверяем, если текущий фон находится ниже камеры (игрока)
        if (playerTransform.position.y - backgrounds[currentBackgroundIndex].transform.position.y > backgroundHeight)
        {
            // Перемещаем фон, который находится ниже всех, вперед (вверх)
            RepositionBackground();
        }
    }

    // Метод для перемещения фона вперед (вверх)
    private void RepositionBackground()
    {
        if (backgrounds.Length == 0) return; // Проверка на наличие фонов в массиве

        // Индекс фона, который находится выше всех (самый последний)
        int farthestBackgroundIndex = (currentBackgroundIndex + backgrounds.Length - 1) % backgrounds.Length;

        // Перемещаем текущий фон вперед на высоту всех фонов, чтобы он следовал за самым дальним фоном
        backgrounds[currentBackgroundIndex].transform.position = backgrounds[farthestBackgroundIndex].transform.position + new Vector3(0, backgroundHeight, 0);

        // Обновляем индекс текущего фона
        currentBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
    }

    // Метод для автоматического определения высоты фона
    private float GetBackgroundHeight()
    {
        if (backgroundPrefab != null)
        {
            // Попробуем получить компонент SpriteRenderer для определения высоты спрайта
            SpriteRenderer spriteRenderer = backgroundPrefab.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Если есть SpriteRenderer, возвращаем высоту спрайта в мировых координатах
                return spriteRenderer.bounds.size.y;
            }
        }

        Debug.LogWarning("SpriteRenderer не найден на backgroundPrefab! Убедитесь, что на фоне есть компонент SpriteRenderer.");
        return 0f;
    }

}
