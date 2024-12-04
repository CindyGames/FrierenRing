using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Метод для перезапуска игры (или уровня)
    public void Restart()
    {
        GameObject but = GameObject.FindGameObjectWithTag("Button Reward");
        if (but != null)
        {
            but.gameObject.SetActive(false);
        }

        // Возобновляем время при перезапуске игры

        // Перезагрузка текущей сцены
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        
        SceneManager.LoadScene(0);
    }
}
