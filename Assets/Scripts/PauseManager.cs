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
    // ����� ��� ���������� ���� �� �����
    public void PauseGame()
    {
        Time.timeScale = 0f;          // ������������� �����
    }

    // ����� ��� ������������� ����
    public void ResumeGame()
    {
        Time.timeScale = 1f;          // ���������� ����� � �����
    }

    // ����� ��� ������ �� ���� (����� �������� �� ����� � ����)
    public void QuitGame()
    {
        // ��� ���������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ��� ������ ����
        Application.Quit();
#endif
    }
}
