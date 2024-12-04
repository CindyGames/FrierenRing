using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Для перезагрузки сцены
using TMPro;  // Для работы с TextMeshPro

public class ResetGameProgress : MonoBehaviour
{
    public TextMeshProUGUI buttonText;  // Ссылка на текст кнопки
    private bool isConfirmationStage = false;  // Проверка, находимся ли мы на стадии подтверждения
    private string initialText = "Сбросить игровой прогресс";  // Начальный текст кнопки
    private string confirmationText = "Вы уверены?";  // Текст для подтверждения

    void Start()
    {
        // Устанавливаем начальный текст кнопки
        if (buttonText != null)
        {
            buttonText.text = initialText;
        }
    }

    // Метод, вызываемый при нажатии кнопки
    public void OnResetButtonClick()
    {
        if (!isConfirmationStage)
        {
            // Переходим на стадию подтверждения
            isConfirmationStage = true;
            if (buttonText != null)
            {
                buttonText.text = confirmationText;
            }
        }
        else
        {
            // Сбрасываем прогресс и перезагружаем сцену
            ResetAllData();
            ReloadScene();
        }
    }

    // Метод для сброса всех сохранений
    private void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All game progress has been reset.");
    }

    // Метод для перезагрузки текущей сцены
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
