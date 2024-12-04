using UnityEngine;
using UnityEngine.UI; // Для работы с кнопками
using UnityEngine.SceneManagement; // Для загрузки уровней

public class LevelLoaderButton : MonoBehaviour
{
    private Button levelButton; // Кнопка для загрузки уровня
    public int levelNumber; // Номер уровня, который загружаем
    private string levelKey; // Ключ для проверки состояния уровня

    void Start()
    {
        levelButton = GetComponent<Button>();

        // Генерируем ключ для сохранения состояния уровня
        levelKey = "lvl " + levelNumber + "_Unlocked";

        // Проверяем, открыт ли уровень
        if (IsLevelUnlocked())
        {
            levelButton.interactable = true; // Активируем кнопку, если уровень открыт
            levelButton.onClick.AddListener(LoadLevel); // Добавляем загрузку уровня на кнопку
        }
        else
        {
            levelButton.interactable = false; // Отключаем кнопку, если уровень закрыт
        }
    }

    // Метод для загрузки уровня
    void LoadLevel()
    {
        // Загружаем уровень по имени, например "lvl 1", "lvl 2" и так далее
        string levelName = "lvl " + levelNumber;
        SceneManager.LoadScene(levelName);
    }

    // Проверяем, открыт ли уровень
    bool IsLevelUnlocked()
    {
        // Возвращает 1, если уровень открыт, или 0, если он закрыт
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }
}
