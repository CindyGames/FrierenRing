using UnityEngine;
using UnityEngine.SceneManagement; // Для работы с загрузкой сцен
using UnityEngine.UI; // Для работы с кнопками

public class NextLevelButton : MonoBehaviour
{
    private Button nextLevelButton; // Кнопка для загрузки следующего уровня

    void Start()
    {
        nextLevelButton = GetComponent<Button>();

        // Добавляем событие к кнопке для загрузки следующего уровня
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Collider2D>().enabled = false;
    }
    // Метод для загрузки следующего уровня
    void LoadNextLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<GamePauseManager>().PauseGame();
        player.GetComponent<Collider2D>().enabled = true;

        player.transform.position = Vector3.zero;

        GameObject but = GameObject.FindGameObjectWithTag("Button Reward");
        if (but != null)
        {
            but.gameObject.SetActive(false);
        }

        // Получаем текущую сцену
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;

        // Проверяем, существует ли следующий уровень
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Загружаем следующий уровень по индексу
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        
    }
}
