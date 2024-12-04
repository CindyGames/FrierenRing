using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDontDestroy : MonoBehaviour
{
    public static PlayerDontDestroy instance; // Singleton instance

    void Awake()
    {
        // Проверяем, существует ли уже экземпляр игрока
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожаем игрока между сценами
        }
        else
        {
            Destroy(instance.gameObject); // Уничтожаем существующего игрока
            instance = this; // Обновляем ссылку на текущий объект
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезагружаем сцену
        }
    }

    public void OnPlayerDefeated()
    {
        Destroy(gameObject); // Уничтожаем игрока
        SceneManager.LoadScene(0); // Перезагружаем сцену
    }
}
