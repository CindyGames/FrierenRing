using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject victoryScreen;    // Экран победы, который будет активирован
    public float victoryDelay = 4;     // Задержка перед активацией экрана победы

    private bool isLevelCompleted = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если игрок касается триггера
        if (other.CompareTag("Player") && !isLevelCompleted)
        {
            isLevelCompleted = true;
            StartCoroutine(EndLevelCoroutine());
        }
    }

    // Сопрограмма, которая активирует экран победы через задержку
    IEnumerator EndLevelCoroutine()
    {
        yield return new WaitForSeconds(victoryDelay);

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

    }

}
