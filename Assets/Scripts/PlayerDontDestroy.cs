using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDontDestroy : MonoBehaviour
{
    public static PlayerDontDestroy instance; // Singleton instance

    void Awake()
    {
        // ���������, ���������� �� ��� ��������� ������
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ������ ����� �������
        }
        else
        {
            Destroy(instance.gameObject); // ���������� ������������� ������
            instance = this; // ��������� ������ �� ������� ������
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ������������� �����
        }
    }

    public void OnPlayerDefeated()
    {
        Destroy(gameObject); // ���������� ������
        SceneManager.LoadScene(0); // ������������� �����
    }
}
