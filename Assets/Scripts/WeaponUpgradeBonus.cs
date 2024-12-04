using UnityEngine;

public class WeaponUpgradeBonus : MonoBehaviour
{
    public enum UpgradeType
    {
        AttackRate,
        Damage,
        BulletsPerShot
    }

    public UpgradeType upgradeType; // Тип улучшения, который применит бонус
    public float attackRateIncrease = 0.1f; // Уменьшение времени между выстрелами (увеличение скорости)
    public int damageIncrease = 1; // Увеличение урона
    public int bulletsPerShotIncrease = 1; // Увеличение количества пуль в очереди

    private PlayerWeaponManager weaponManager;

    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, если бонус собирает игрок
        if (other.CompareTag("Player"))
        {

            if (weaponManager != null)
            {
                ApplyUpgrade(); // Применяем улучшение к выбранной характеристике
                
            }
        }
    }


    public void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case UpgradeType.AttackRate:
                weaponManager.attackRate = Mathf.Max(0.1f, weaponManager.attackRate + attackRateIncrease); // Ограничение минимального значения
                break;
            case UpgradeType.Damage:
                weaponManager.damage += damageIncrease;
                break;
            case UpgradeType.BulletsPerShot:
                weaponManager.bulletsPerShot += bulletsPerShotIncrease;
                break;
        }

        GetComponent<AudioSource>().Play();

        Destroy(gameObject, 0.5f); // Удаляем бонус после его использования


    }
}
