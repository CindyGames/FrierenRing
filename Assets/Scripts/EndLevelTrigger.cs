using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject victoryScreen;    // ����� ������, ������� ����� �����������
    public float victoryDelay = 4;     // �������� ����� ���������� ������ ������

    private bool isLevelCompleted = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� ����� �������� ��������
        if (other.CompareTag("Player") && !isLevelCompleted)
        {
            isLevelCompleted = true;
            StartCoroutine(EndLevelCoroutine());
        }
    }

    // �����������, ������� ���������� ����� ������ ����� ��������
    IEnumerator EndLevelCoroutine()
    {
        yield return new WaitForSeconds(victoryDelay);

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

    }

}
