using UnityEngine;
using UnityEngine.UI; // ��� ������ � ��������
using UnityEngine.SceneManagement; // ��� �������� �������

public class LevelLoaderButton : MonoBehaviour
{
    private Button levelButton; // ������ ��� �������� ������
    public int levelNumber; // ����� ������, ������� ���������
    private string levelKey; // ���� ��� �������� ��������� ������

    void Start()
    {
        levelButton = GetComponent<Button>();

        // ���������� ���� ��� ���������� ��������� ������
        levelKey = "lvl " + levelNumber + "_Unlocked";

        // ���������, ������ �� �������
        if (IsLevelUnlocked())
        {
            levelButton.interactable = true; // ���������� ������, ���� ������� ������
            levelButton.onClick.AddListener(LoadLevel); // ��������� �������� ������ �� ������
        }
        else
        {
            levelButton.interactable = false; // ��������� ������, ���� ������� ������
        }
    }

    // ����� ��� �������� ������
    void LoadLevel()
    {
        // ��������� ������� �� �����, �������� "lvl 1", "lvl 2" � ��� �����
        string levelName = "lvl " + levelNumber;
        SceneManager.LoadScene(levelName);
    }

    // ���������, ������ �� �������
    bool IsLevelUnlocked()
    {
        // ���������� 1, ���� ������� ������, ��� 0, ���� �� ������
        return PlayerPrefs.GetInt(levelKey, 0) == 1;
    }
}
