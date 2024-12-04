using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeButton : MonoBehaviour
{
    public int weaponIndex; // Индекс оружия, который активируется при нажатии на кнопку

    private void Start()
    {
        // Получаем компонент Button и добавляем слушатель нажатия
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // Находим объект игрока на сцене
        PlayerWeaponManager playerWeaponManager = Object.FindFirstObjectByType<PlayerWeaponManager>();

        if (playerWeaponManager != null)
        {
            // Активируем оружие по указанному индексу
            playerWeaponManager.ActivateWeapon(weaponIndex);
        }
        else
        {
            Debug.LogWarning("PlayerWeaponManager не найден на сцене.");
        }
    }
}
