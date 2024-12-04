using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance; // Singleton для удобного доступа
    private List<WeaponShop> allWeapons = new List<WeaponShop>(); // Список всех оружий
    private WeaponShop equippedWeapon; // Текущее экипированное оружие

    private PlayerShooting playerShooting; // Ссылка на скрипт стрельбы
    private const string EquippedWeaponKey = "EquippedWeapon"; // Ключ для сохранения экипированного оружия

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

        // Запуск загрузки экипированного оружия с небольшой задержкой
        StartCoroutine(LoadEquippedWeaponWithDelay());
    }

    // Добавляем оружие в список (вызывается из WeaponShop при создании оружия)
    public void RegisterWeapon(WeaponShop weapon)
    {
        if (!allWeapons.Contains(weapon))
        {
            allWeapons.Add(weapon);
        }
    }

    // Метод для экипировки оружия
    public void EquipWeapon(WeaponShop weapon)
    {
        if (equippedWeapon != null)
        {
            // Если есть уже экипированное оружие, убираем его эффект
            //playerShooting.IncreaseDamage(-equippedWeapon.damageIncrease);
            equippedWeapon.equipButton.interactable = true; // Включаем кнопку экипировки предыдущего оружия
        }

        // Устанавливаем новое экипированное оружие
        equippedWeapon = weapon;

        // Применяем бонус урона от нового оружия
        //playerShooting.IncreaseDamage(equippedWeapon.damageIncrease);

        // Делаем кнопку экипировки неактивной для текущего оружия
        equippedWeapon.equipButton.interactable = false;

        // Сохраняем экипированное оружие
        SaveEquippedWeapon(weapon.weaponName);
    }

    // Метод для сохранения экипированного оружия
    private void SaveEquippedWeapon(string weaponName)
    {
        PlayerPrefs.SetString(EquippedWeaponKey, weaponName);
        PlayerPrefs.Save();
    }

    // Метод для загрузки экипированного оружия с задержкой
    private IEnumerator LoadEquippedWeaponWithDelay()
    {
        // Небольшая задержка, чтобы все оружия успели зарегистрироваться
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
