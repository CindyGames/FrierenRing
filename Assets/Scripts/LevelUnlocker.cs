using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    private string levelKey; // Ключ для сохранения состояния уровня

    void Start()
    {
        // Получаем имя текущей сцены (уровня)
        string currentLevelName = SceneManager.GetActiveScene().name;

        // Генерируем ключ для сохранения состояния уровня на основе его имени
        levelKey = currentLevelName + "_Unlocked";

        // Сохраняем уровень, если он ещё не был открыт
        if (!IsLevelUnlocked())
        {
            UnlockLevel();
        }
    }

    // Проверяем, открыт ли уровень
    bool IsLevelUnlocked()
    {
        // Возвращает 1, если уровень уже открыт, или 0, если он закрыт
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    // Сохраняем уровень как открытый
    void UnlockLevel()
    {
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save(); // Сохраняем изменения
    }
}
