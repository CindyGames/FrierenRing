using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public PlayerLaserShooter laserShooter;
    public HomingShooting rocket;

    public float attackRate = 1;
    public int damage = 1;
    public int bulletsPerShot = 0;

    public List<GameObject> weapons;
    public int currentWeaponIndex = 0;

    private bool laserWasActive;
    private bool rocketWasActive;

    public void AddDamage(int damage)
    {
        this.damage += damage;
    }

    public void AddBullet()
    {
        bulletsPerShot++;
    }

    public void AddLaserLvl()
    {
        if (laserShooter.isActiveAndEnabled)
        {

            laserShooter.LvlUp();
        }
        else
        {
            laserShooter.gameObject.SetActive(true);
        }
    }

    public void AddRocketLvl()
    {
        if (rocket.isActiveAndEnabled)
        {

            rocket.LvlUp();
        }
        else
        {
            rocket.gameObject.SetActive(true);
        }

    }

    void Start()
    {
        ActivateWeapon(currentWeaponIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
            ActivateWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count > 2)
            ActivateWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Count > 3)
            ActivateWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Count > 4)
            ActivateWeapon(4);
    }

    public void ActivateWeapon(int index)
    {
        // Сохраняем начальное состояние лазера и ракеты
        laserWasActive = laserShooter != null && laserShooter.gameObject.activeSelf;
        rocketWasActive = rocket != null && rocket.gameObject.activeSelf;

        DisableAllWeapons();

        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;
            weapons[currentWeaponIndex].SetActive(true);
        }

        ReEnablePreviousWeapons();
    }

    public void ActivateCurrentWeapon()
    {
        DisableAllWeapons();

        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].SetActive(true);
        }

        ReEnablePreviousWeapons();
    }

    public void ReduceSpreadForAllWeapons()
    {
        foreach (Transform child in transform)
        {
            PlayerShooting shootingComponent = child.GetComponent<PlayerShooting>();
            if (shootingComponent != null)
            {
                shootingComponent.maxSpreadAngle *= 0.7f;
            }
        }
    }

    public void DisableAllWeapons()
    {
        // Сохраняем текущий статус лазера и ракеты, если это не было сделано ранее
        if (laserShooter != null)
        {
            if (!laserWasActive)
            {
                laserWasActive = laserShooter.gameObject.activeSelf;
            }
            laserShooter.gameObject.SetActive(false);
        }

        if (rocket != null)
        {
            if (!rocketWasActive)
            {
                rocketWasActive = rocket.gameObject.activeSelf;
            }
            rocket.gameObject.SetActive(false);
        }

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }

    void ReEnablePreviousWeapons()
    {
        if (laserShooter != null && laserWasActive)
        {
            laserShooter.gameObject.SetActive(true);
        }

        if (rocket != null && rocketWasActive)
        {
            rocket.gameObject.SetActive(true);
        }
    }
}
