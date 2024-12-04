using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    private string levelKey; // ���� ��� ���������� ��������� ������

    void Start()
    {
        // �������� ��� ������� ����� (������)
        string currentLevelName = SceneManager.GetActiveScene().name;

        // ���������� ���� ��� ���������� ��������� ������ �� ������ ��� �����
        levelKey = currentLevelName + "_Unlocked";

        // ��������� �������, ���� �� ��� �� ��� ������
        if (!IsLevelUnlocked())
        {
            UnlockLevel();
        }
    }

    // ���������, ������ �� �������
    bool IsLevelUnlocked()
    {
        // ���������� 1, ���� ������� ��� ������, ��� 0, ���� �� ������
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }

    // ��������� ������� ��� ��������
    void UnlockLevel()
    {
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save(); // ��������� ���������
    }
}
