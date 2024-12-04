using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private void Start()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    // Метод для постановки игры на паузу
    public void PauseGame()
    {
        Time.timeScale = 0f;          // Останавливаем время
    }

    // Метод для возобновления игры
    public void ResumeGame()
    {
        Time.timeScale = 1f;          // Возвращаем время в норму
    }

    // Метод для выхода из игры (можно заменить на выход в меню)
    public void QuitGame()
    {
        // Для редактора
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Для сборки игры
        Application.Quit();
#endif
    }
}
