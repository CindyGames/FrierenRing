using TMPro;
using UnityEngine;

public class BossReward : MonoBehaviour
{
    public string bossID = "Boss1"; // ���������� ������������� �����, ����� ��������� ��� �� ������ ������
    public int rewardAmount = 100; // ���������� ������� �� ������ ��� ������
    private string victoryKey; // ���� ��� ��������, �������� �� ����

    public TMP_Text textReward;

    void Start()
    {
        // ���������� ���� ��� ���������� ��������� ������ ��� ������
        victoryKey = bossID + "_Defeated";
    }

    // �����, ���������� ��� ������ ��� ������
    public void OnBossDefeated()
    {
        // ���������, �������� �� ����� ����� ����� �����
        if (!IsBossDefeated())
        {
            textReward.gameObject.SetActive(true);

            // ����� �������, ���� ���� ������� �������
            GiveReward();
            MarkBossAsDefeated(); // ��������� ��������� ������
        }
        else
        {
            Debug.Log("���� ��� �������. ������� �� ������.");
        }
    }

    // ���������, �������� �� ����� �����
    bool IsBossDefeated()
    {
        return PlayerPrefs.GetInt(victoryKey, 0) == 1; // ���������� 1, ���� ���� ��� �������, ��� 0, ���� ���
    }

    // ��������� ��������� ������ ��� ������
    void MarkBossAsDefeated()
    {
        PlayerPrefs.SetInt(victoryKey, 1); // �������� ����� ��� �����������
        PlayerPrefs.Save(); // ��������� ���������
    }

    // ����� ��� ������ ������� ������
    void GiveReward()
    {

        // ����� ����� ����������� ������ ������ �������, ��������, ��������� ���������� ����� � ������
        Debug.Log("������� ������: " + rewardAmount);
        // ������: PlayerStats.Instance.AddCoins(rewardAmount);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>().AddGold(rewardAmount);
    }
}
