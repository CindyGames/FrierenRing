using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance; // Singleton ��� �������� �������
    private List<WeaponShop> allWeapons = new List<WeaponShop>(); // ������ ���� ������
    private WeaponShop equippedWeapon; // ������� ������������� ������

    private PlayerShooting playerShooting; // ������ �� ������ ��������
    private const string EquippedWeaponKey = "EquippedWeapon"; // ���� ��� ���������� �������������� ������

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponent<PlayerShooting>();

        // ������ �������� �������������� ������ � ��������� ���������
        StartCoroutine(LoadEquippedWeaponWithDelay());
    }

    // ��������� ������ � ������ (���������� �� WeaponShop ��� �������� ������)
    public void RegisterWeapon(WeaponShop weapon)
    {
        if (!allWeapons.Contains(weapon))
        {
            allWeapons.Add(weapon);
        }
    }

    // ����� ��� ���������� ������
    public void EquipWeapon(WeaponShop weapon)
    {
        if (equippedWeapon != null)
        {
            // ���� ���� ��� ������������� ������, ������� ��� ������
            //playerShooting.IncreaseDamage(-equippedWeapon.damageIncrease);
            equippedWeapon.equipButton.interactable = true; // �������� ������ ���������� ����������� ������
        }

        // ������������� ����� ������������� ������
        equippedWeapon = weapon;

        // ��������� ����� ����� �� ������ ������
        //playerShooting.IncreaseDamage(equippedWeapon.damageIncrease);

        // ������ ������ ���������� ���������� ��� �������� ������
        equippedWeapon.equipButton.interactable = false;

        // ��������� ������������� ������
        SaveEquippedWeapon(weapon.weaponName);
    }

    // ����� ��� ���������� �������������� ������
    private void SaveEquippedWeapon(string weaponName)
    {
        PlayerPrefs.SetString(EquippedWeaponKey, weaponName);
        PlayerPrefs.Save();
    }

    // ����� ��� �������� �������������� ������ � ���������
    private IEnumerator LoadEquippedWeaponWithDelay()
    {
        // ��������� ��������, ����� ��� ������ ������ ������������������
        yield return new WaitForSeconds(0.1f);

        if (PlayerPrefs.HasKey(EquippedWeaponKey))
        {
            string savedWeaponName = PlayerPrefs.GetString(EquippedWeaponKey);
            WeaponShop savedWeapon = allWeapons.Find(w => w.weaponName == savedWeaponName);
            if (savedWeapon != null)
            {
                EquipWeapon(savedWeapon);
            }
        }
    }
}
