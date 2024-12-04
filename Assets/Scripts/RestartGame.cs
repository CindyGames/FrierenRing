using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // ����� ��� ����������� ���� (��� ������)
    public void Restart()
    {
        GameObject but = GameObject.FindGameObjectWithTag("Button Reward");
        if (but != null)
        {
            but.gameObject.SetActive(false);
        }

        // ������������ ����� ��� ����������� ����

        // ������������ ������� �����
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        
        SceneManager.LoadScene(0);
    }
}
