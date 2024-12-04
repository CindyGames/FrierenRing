using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ��� ������ � TextMeshPro

public class LevelUpButton : MonoBehaviour
{
    public Button levelUpButton;          // ������ �� ��������� ������
    public CharacterStats characterStats; // ������ �� ��������� CharacterStats ���������
    public PlayerGold playerGold;         // ������ �� ��������� PlayerGold ��� �������� ������
    public int baseIncreaseAmount = 1;    // ������� ����������, �� ������� ����������� ����
    public int baseUpgradeCost = 10;      // ������� ��������� ��������� ������
    private int currentUpgradeCost;       // ������� ��������� ���������
    private int currentIncreaseAmount;    // ������� ���������� ���������� ����
    public float costMultiplier = 1.5f;   // ��������� ��� ���������� ��������� ���������
    public float powerMultiplier = 1.2f;  // ��������� ��� ���������� ����

    public TextMeshProUGUI upgradeCostText;  // ������ �� TextMeshProUGUI ��� ����������� ���������
    public TextMeshProUGUI powerIncreaseText; // ������ �� TextMeshProUGUI ��� ����������� ���������� ����

    private const string PowerKey = "PlayerPower";           // ���� ��� ���������� ���� ������
    private const string UpgradeCostKey = "UpgradeCost";     // ���� ��� ���������� ��������� ���������
    private const string IncreaseAmountKey = "IncreaseAmount"; // ���� ��� ���������� ���������� ���������� ����

    void Start()
    {
        if (levelUpButton == null)
        {
            levelUpButton = GetComponent<Button>();
        }

        // ��������� ����������� ������
        LoadData();

        // ��������� ����������� ���� � ���������
        if (characterStats != null)
        {
            characterStats.SetPower(PlayerPrefs.GetInt(PowerKey, baseIncreaseAmount));
        }

        // ��������� ����� ��������� ��������� � ����
        UpdateUpgradeCostText();

        // ����������� ����� �� ������� ������� ������
        levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
    }

    // �����, ������� ���������� ��� ������� �� ������
    void OnLevelUpButtonClick()
    {
        // ���������, ���������� �� ������ ��� ��������� ������
        if (playerGold != null && playerGold.gold >= currentUpgradeCost)
        {
            // ��������� ���������� ������ �� ��������� ���������
            playerGold.AddGold(-currentUpgradeCost);

            // �������� ���� ���������
            if (characterStats != null)
            {
                characterStats.IncreasePower(currentIncreaseAmount);
                // ��������� ����� ���� ���������
                PlayerPrefs.SetInt(PowerKey, characterStats.strength);
            }

            // ����������� ��������� ���������� ������
            currentUpgradeCost = Mathf.CeilToInt(currentUpgradeCost * costMultiplier);
            PlayerPrefs.SetInt(UpgradeCostKey, currentUpgradeCost); // ��������� ��������� ���������

            // ����������� ���������� ���� ��� ���������� ������
            currentIncreaseAmount = Mathf.CeilToInt(currentIncreaseAmount * powerMultiplier);
            PlayerPrefs.SetInt(IncreaseAmountKey, currentIncreaseAmount); // ��������� ���������� ���������� ����

            // ��������� ����� ��������� ��������� � ���������� ����
            UpdateUpgradeCostText();
        }
        else
        {
            Debug.Log("Not enough gold to upgrade.");
        }
    }

    // ����� ��� ���������� ������ ��������� ��������� � ���������� ����
    void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            upgradeCostText.text = currentUpgradeCost.ToString();
        }

        if (powerIncreaseText != null)
        {
            powerIncreaseText.text = "+ " + currentIncreaseAmount.ToString();
        }
    }

    // ����� ��� �������� ����������� ������
    void LoadData()
    {
        // ��������� ��������� ���������, ���� ��� ���� ���������
        currentUpgradeCost = PlayerPrefs.GetInt(UpgradeCostKey, baseUpgradeCost);

        // ��������� ���������� ���������� ����, ���� ��� ���� ���������
        currentIncreaseAmount = PlayerPrefs.GetInt(IncreaseAmountKey, baseIncreaseAmount);
    }
}
