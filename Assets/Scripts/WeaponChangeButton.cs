using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeButton : MonoBehaviour
{
    public int weaponIndex; // ������ ������, ������� ������������ ��� ������� �� ������

    private void Start()
    {
        // �������� ��������� Button � ��������� ��������� �������
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // ������� ������ ������ �� �����
        PlayerWeaponManager playerWeaponManager = Object.FindFirstObjectByType<PlayerWeaponManager>();

        if (playerWeaponManager != null)
        {
            // ���������� ������ �� ���������� �������
            playerWeaponManager.ActivateWeapon(weaponIndex);
        }
        else
        {
            Debug.LogWarning("PlayerWeaponManager �� ������ �� �����.");
        }
    }
}
