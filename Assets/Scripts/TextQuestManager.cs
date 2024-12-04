using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string questionText;                     // Текст квеста или вопроса
    public List<string> answerOptions;              // Варианты ответов
    public List<int> nextQuestIndexes;              // Индексы следующих вопросов для каждого варианта
    public int damageAnswerIndex = -1;              // Индекс ответа, наносящего урон (-1, если нет)
    public int damageAmount = 0;                    // Количество урона, если выбран ответ с индексом damageAnswerIndex
    public int goldAnswerIndex = -1;                // Индекс ответа, дающего золото (-1, если нет)
    public int goldAmount = 0;                      // Количество золота, если выбран ответ с индексом goldAnswerIndex
    public int correctAnswerIndex = -1;             // Индекс правильного ответа (-1, если нет правильного)
}

public class TextQuestManager : MonoBehaviour
{
    public TextMeshProUGUI questionTextUI;          // UI компонент для отображения текста вопроса
    public Button[] answerButtons;                  // Кнопки для выбора ответов
    public List<Quest> quests;                      // Список всех квестов
    private int currentQuestIndex = 0;              // Индекс текущего вопроса

    private int playerHealth = 100;                 // Начальное здоровье игрока
    private int playerGold = 0;                     // Начальное количество золота

    void Start()
    {
        DisplayQuest();
    }

    void DisplayQuest()
    {
        if (currentQuestIndex < 0 || currentQuestIndex >= quests.Count)
        {
            Debug.LogWarning("Квест завершен или неверный индекс!");
            questionTextUI.text = "Квест завершен!";
            foreach (Button btn in answerButtons) btn.gameObject.SetActive(false);
            return;
        }

        Quest currentQuest = quests[currentQuestIndex];
        questionTextUI.text = currentQuest.questionText;

        // Настраиваем кнопки ответов
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuest.answerOptions.Count)
            {
                answerButtons[i].gameObject.SetActive(true); // Включаем кнопку, если вариант доступен
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuest.answerOptions[i];

                int nextQuest = currentQuest.nextQuestIndexes[i]; // Следующий квест
                int answerIndex = i;  // Сохраняем индекс ответа для использования в обработчике
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(nextQuest, answerIndex == currentQuest.correctAnswerIndex, answerIndex));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // Отключаем лишние кнопки
            }
        }
    }

    void OnAnswerSelected(int nextQuestIndex, bool isCorrect, int answerIndex)
    {
        Quest currentQuest = quests[currentQuestIndex];

        // Проверяем, если ответ совпадает с индексом урона
        if (answerIndex == currentQuest.damageAnswerIndex)
        {
            playerHealth -= currentQuest.damageAmount;
            Debug.Log("Игрок получил урон: " + currentQuest.damageAmount + ". Текущее здоровье: " + playerHealth);
        }

        // Проверяем, если ответ совпадает с индексом золота
        if (answerIndex == currentQuest.goldAnswerIndex)
        {
            playerGold += currentQuest.goldAmount;
            Debug.Log("Игрок получил золото: " + currentQuest.goldAmount + ". Текущее золото: " + playerGold);
        }

        // Проверка правильности ответа
        if (isCorrect)
        {
            Debug.Log("Правильный ответ!");
        }
        else
        {
            Debug.Log("Неправильный ответ...");
        }

        // Переход к следующему квесту
        currentQuestIndex = nextQuestIndex >= 0 ? nextQuestIndex : currentQuestIndex;
        DisplayQuest();
    }
}
