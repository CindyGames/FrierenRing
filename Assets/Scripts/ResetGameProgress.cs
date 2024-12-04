using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ��� ������������ �����
using TMPro;  // ��� ������ � TextMeshPro

public class ResetGameProgress : MonoBehaviour
{
    public TextMeshProUGUI buttonText;  // ������ �� ����� ������
    private bool isConfirmationStage = false;  // ��������, ��������� �� �� �� ������ �������������
    private string initialText = "�������� ������� ��������";  // ��������� ����� ������
    private string confirmationText = "�� �������?";  // ����� ��� �������������

    void Start()
    {
        // ������������� ��������� ����� ������
        if (buttonText != null)
        {
            buttonText.text = initialText;
        }
    }

    // �����, ���������� ��� ������� ������
    public void OnResetButtonClick()
    {
        if (!isConfirmationStage)
        {
            // ��������� �� ������ �������������
            isConfirmationStage = true;
            if (buttonText != null)
            {
                buttonText.text = confirmationText;
            }
        }
        else
        {
            // ���������� �������� � ������������� �����
            ResetAllData();
            ReloadScene();
        }
    }

    // ����� ��� ������ ���� ����������
    private void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All game progress has been reset.");
    }

    // ����� ��� ������������ ������� �����
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
