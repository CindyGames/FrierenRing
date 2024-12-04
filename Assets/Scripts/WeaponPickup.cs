using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex; // ������ ������, �� ������� ����� ������������� ��� ������� ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���������� �� ����� � �������
        if (other.CompareTag("Player"))
        {
            // �������� ��������� PlayerWeaponManager � ������
            PlayerWeaponManager playerWeaponManager = other.GetComponent<PlayerWeaponManager>();

            if (playerWeaponManager != null)
            {
                // ���������� ��������� ������
                playerWeaponManager.ActivateWeapon(weaponIndex);
            }

            // ���������� ����� ����� �������������
            Destroy(gameObject);
        }
    }
}
