using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������ � ��������� ����
using UnityEngine.UI; // ��� ������ � ��������

public class NextLevelButton : MonoBehaviour
{
    private Button nextLevelButton; // ������ ��� �������� ���������� ������

    void Start()
    {
        nextLevelButton = GetComponent<Button>();

        // ��������� ������� � ������ ��� �������� ���������� ������
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Collider2D>().enabled = false;
    }
    // ����� ��� �������� ���������� ������
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

        // �������� ������� �����
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;

        // ���������, ���������� �� ��������� �������
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // ��������� ��������� ������� �� �������
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        
    }
}
