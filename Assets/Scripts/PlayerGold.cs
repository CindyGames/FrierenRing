using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int gold = 0; // ���������� ������
    public TextMeshProUGUI goldText; // ��������� ���� ��� ����������� ���������� ������

    public AudioSource audioSource;

    private const string GoldKey = "PlayerGold"; // ���� ��� �������� ������ � PlayerPrefs

    void Start()
    {
        // ��������� ���������� ������ ��� ������ ������
        LoadGold();
        UpdateGoldText();
    }

    // ����� ��� ���������� ���������� ������
    public void AddGold(int amount)
    {
        PlaySound(); // ����� ��� ��������������� �����

        gold += amount;
        UpdateGoldText();
        SaveGold(); // ��������� ������ ����� ���������
                    

    }

    // ����� ��� ���������� ������ � ����������� ������
    void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString();
        }
    }

    // ����� ��� ���������� ���������� ������
    void SaveGold()
    {
        PlayerPrefs.SetInt(GoldKey, gold);
        PlayerPrefs.Save();
    }

    // ����� ��� �������� ������������ ���������� ������
    void LoadGold()
    {
        if (PlayerPrefs.HasKey(GoldKey))
        {
            gold = PlayerPrefs.GetInt(GoldKey);
        }
    }

    // ����� ��� ��������������� �����
    public void PlaySound()
    {
        if (audioSource != null)
        {
            // ��������������� �����
            audioSource.Play();
        }
    }
}
