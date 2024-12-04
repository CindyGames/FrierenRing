using UnityEngine;

public class WeaponUpgradeBonus : MonoBehaviour
{
    public enum UpgradeType
    {
        AttackRate,
        Damage,
        BulletsPerShot
    }

    public UpgradeType upgradeType; // ��� ���������, ������� �������� �����
    public float attackRateIncrease = 0.1f; // ���������� ������� ����� ���������� (���������� ��������)
    public int damageIncrease = 1; // ���������� �����
    public int bulletsPerShotIncrease = 1; // ���������� ���������� ���� � �������

    private PlayerWeaponManager weaponManager;

    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� ����� �������� �����
        if (other.CompareTag("Player"))
        {

            if (weaponManager != null)
            {
                ApplyUpgrade(); // ��������� ��������� � ��������� ��������������
                
            }
        }
    }


    public void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case UpgradeType.AttackRate:
                weaponManager.attackRate = Mathf.Max(0.1f, weaponManager.attackRate + attackRateIncrease); // ����������� ������������ ��������
                break;
            case UpgradeType.Damage:
                weaponManager.damage += damageIncrease;
                break;
            case UpgradeType.BulletsPerShot:
                weaponManager.bulletsPerShot += bulletsPerShotIncrease;
                break;
        }

        GetComponent<AudioSource>().Play();

        Destroy(gameObject, 0.5f); // ������� ����� ����� ��� �������������


    }
}
