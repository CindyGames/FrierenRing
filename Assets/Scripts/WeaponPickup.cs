using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex; // Индекс оружия, на которое нужно переключиться при подборе бонуса

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, столкнулся ли бонус с игроком
        if (other.CompareTag("Player"))
        {
            // Получаем компонент PlayerWeaponManager у игрока
            PlayerWeaponManager playerWeaponManager = other.GetComponent<PlayerWeaponManager>();

            if (playerWeaponManager != null)
            {
                // Активируем указанное оружие
                playerWeaponManager.ActivateWeapon(weaponIndex);
            }

            // Уничтожаем бонус после использования
            Destroy(gameObject);
        }
    }
}
