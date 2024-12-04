using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public string weaponName;              // �������� ������
    public int damageIncrease;             // ���������� �����
    public int cost;                       // ��������� ������
    public Button buyButton;               // ������ �������
    public Button equipButton;             // ������ ����������
    public TextMeshProUGUI weaponCostText; // ��������� ���� ��� ����������� ��������� ������
    public TextMeshProUGUI description;

    private PlayerGold playerGold;         // ������ �� ��������� ��� ���������� �������
    private TextMeshProUGUI buyButtonText; // ����� �� ������ �������
    private const string PurchasedKeyPrefix = "PurchasedWeapon_"; // ������� ��� ���������� ������ � �������

    void Start()
    {
        // ������������ ��� ������ � WeaponManager
        WeaponManager.Instance.RegisterWeapon(this);


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerGold = player.GetComponent<PlayerGold>();

        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();



        // ���������, ���� �� ��� ������ ������� �����
        if (PlayerPrefs.HasKey(PurchasedKeyPrefix + weaponName))
        {
            DisableBuyButton();
            EnableEquipButton();
        }
        else
        {
            // ������ ��� �� �������, ���������� ������ �������
            buyButton.onClick.AddListener(BuyWeapon);
        }

        UpdateDamageText();
        UpdateWeaponCostText();
    }

    // ����� ��� ������� ������
    public void BuyWeapon()
    {
        if (playerGold.gold >= cost)
        {
            // ������� ������
            playerGold.AddGold(-cost);

            // ��������� ���� �������
            PlayerPrefs.SetInt(PurchasedKeyPrefix + weaponName, 1);
            PlayerPrefs.Save();

            // ��������� ������ �������, ���������� ������ ����������
            DisableBuyButton();
            EnableEquipButton();
        }
        else
        {
            Debug.Log("Not enough gold to buy the weapon.");
        }
    }

    // ����� ��� ���������� ������ �������
    private void DisableBuyButton()
    {
        buyButton.gameObject.SetActive(false);
        buyButtonText.text = "Purchased";
    }

    // ����� ��� ��������� ������ ����������
    private void EnableEquipButton()
    {
        equipButton.gameObject.SetActive(true);
        equipButton.onClick.AddListener(EquipWeapon);
    }

    // ����� ��� ���������� ������
    private void EquipWeapon()
    {
        WeaponManager.Instance.EquipWeapon(this);
        Debug.Log(weaponName + " equipped.");

        // ������ ������ ���������� ���������� ����� ����������
        equipButton.interactable = false;
    }

    // ����� ��� ���������� ������ � ����������� �����
    private void UpdateDamageText()
    {
        description.text = "+" + damageIncrease + " �����";
    }

    // ����� ��� ���������� ���������� ���� � ����� ������
    private void UpdateWeaponCostText()
    {
        if (weaponCostText != null)
        {
            weaponCostText.text = cost.ToString();
        }
    }
}
