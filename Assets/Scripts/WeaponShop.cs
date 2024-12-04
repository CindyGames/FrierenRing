using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public string weaponName;              // Название оружия
    public int damageIncrease;             // Увеличение урона
    public int cost;                       // Стоимость оружия
    public Button buyButton;               // Кнопка покупки
    public Button equipButton;             // Кнопка экипировки
    public TextMeshProUGUI weaponCostText; // Текстовое поле для отображения стоимости оружия
    public TextMeshProUGUI description;

    private PlayerGold playerGold;         // Ссылка на компонент для управления золотом
    private TextMeshProUGUI buyButtonText; // Текст на кнопке покупки
    private const string PurchasedKeyPrefix = "PurchasedWeapon_"; // Префикс для сохранения данных о покупке

    void Start()
    {
        // Регистрируем это оружие в WeaponManager
        WeaponManager.Instance.RegisterWeapon(this);


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerGold = player.GetComponent<PlayerGold>();

        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();



        // Проверяем, было ли это оружие куплено ранее
        if (PlayerPrefs.HasKey(PurchasedKeyPrefix + weaponName))
        {
            DisableBuyButton();
            EnableEquipButton();
        }
        else
        {
            // Оружие еще не куплено, активируем кнопку покупки
            buyButton.onClick.AddListener(BuyWeapon);
        }

        UpdateDamageText();
        UpdateWeaponCostText();
    }

    // Метод для покупки оружия
    public void BuyWeapon()
    {
        if (playerGold.gold >= cost)
        {
            // Снимаем золото
            playerGold.AddGold(-cost);

            // Сохраняем факт покупки
            PlayerPrefs.SetInt(PurchasedKeyPrefix + weaponName, 1);
            PlayerPrefs.Save();

            // Отключаем кнопку покупки, активируем кнопку экипировки
            DisableBuyButton();
            EnableEquipButton();
        }
        else
        {
            Debug.Log("Not enough gold to buy the weapon.");
        }
    }

    // Метод для отключения кнопки покупки
    private void DisableBuyButton()
    {
        buyButton.gameObject.SetActive(false);
        buyButtonText.text = "Purchased";
    }

    // Метод для активации кнопки экипировки
    private void EnableEquipButton()
    {
        equipButton.gameObject.SetActive(true);
        equipButton.onClick.AddListener(EquipWeapon);
    }

    // Метод для экипировки оружия
    private void EquipWeapon()
    {
        WeaponManager.Instance.EquipWeapon(this);
        Debug.Log(weaponName + " equipped.");

        // Делаем кнопку экипировки неактивной после экипировки
        equipButton.interactable = false;
    }

    // Метод для обновления текста с увеличением урона
    private void UpdateDamageText()
    {
        description.text = "+" + damageIncrease + " урона";
    }

    // Метод для обновления текстового поля с ценой оружия
    private void UpdateWeaponCostText()
    {
        if (weaponCostText != null)
        {
            weaponCostText.text = cost.ToString();
        }
    }
}
