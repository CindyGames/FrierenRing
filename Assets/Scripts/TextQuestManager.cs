using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string questionText;                     // ����� ������ ��� �������
    public List<string> answerOptions;              // �������� �������
    public List<int> nextQuestIndexes;              // ������� ��������� �������� ��� ������� ��������
    public int damageAnswerIndex = -1;              // ������ ������, ���������� ���� (-1, ���� ���)
    public int damageAmount = 0;                    // ���������� �����, ���� ������ ����� � �������� damageAnswerIndex
    public int goldAnswerIndex = -1;                // ������ ������, ������� ������ (-1, ���� ���)
    public int goldAmount = 0;                      // ���������� ������, ���� ������ ����� � �������� goldAnswerIndex
    public int correctAnswerIndex = -1;             // ������ ����������� ������ (-1, ���� ��� �����������)
}

public class TextQuestManager : MonoBehaviour
{
    public TextMeshProUGUI questionTextUI;          // UI ��������� ��� ����������� ������ �������
    public Button[] answerButtons;                  // ������ ��� ������ �������
    public List<Quest> quests;                      // ������ ���� �������
    private int currentQuestIndex = 0;              // ������ �������� �������

    private int playerHealth = 100;                 // ��������� �������� ������
    private int playerGold = 0;                     // ��������� ���������� ������

    void Start()
    {
        DisplayQuest();
    }

    void DisplayQuest()
    {
        if (currentQuestIndex < 0 || currentQuestIndex >= quests.Count)
        {
            Debug.LogWarning("����� �������� ��� �������� ������!");
            questionTextUI.text = "����� ��������!";
            foreach (Button btn in answerButtons) btn.gameObject.SetActive(false);
            return;
        }

        Quest currentQuest = quests[currentQuestIndex];
        questionTextUI.text = currentQuest.questionText;

        // ����������� ������ �������
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuest.answerOptions.Count)
            {
                answerButtons[i].gameObject.SetActive(true); // �������� ������, ���� ������� ��������
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuest.answerOptions[i];

                int nextQuest = currentQuest.nextQuestIndexes[i]; // ��������� �����
                int answerIndex = i;  // ��������� ������ ������ ��� ������������� � �����������
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(nextQuest, answerIndex == currentQuest.correctAnswerIndex, answerIndex));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // ��������� ������ ������
            }
        }
    }

    void OnAnswerSelected(int nextQuestIndex, bool isCorrect, int answerIndex)
    {
        Quest currentQuest = quests[currentQuestIndex];

        // ���������, ���� ����� ��������� � �������� �����
        if (answerIndex == currentQuest.damageAnswerIndex)
        {
            playerHealth -= currentQuest.damageAmount;
            Debug.Log("����� ������� ����: " + currentQuest.damageAmount + ". ������� ��������: " + playerHealth);
        }

        // ���������, ���� ����� ��������� � �������� ������
        if (answerIndex == currentQuest.goldAnswerIndex)
        {
            playerGold += currentQuest.goldAmount;
            Debug.Log("����� ������� ������: " + currentQuest.goldAmount + ". ������� ������: " + playerGold);
        }

        // �������� ������������ ������
        if (isCorrect)
        {
            Debug.Log("���������� �����!");
        }
        else
        {
            Debug.Log("������������ �����...");
        }

        // ������� � ���������� ������
        currentQuestIndex = nextQuestIndex >= 0 ? nextQuestIndex : currentQuestIndex;
        DisplayQuest();
    }
}
