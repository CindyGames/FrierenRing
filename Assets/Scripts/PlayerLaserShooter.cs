using System.Collections;
using UnityEngine;

public class PlayerLaserShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка, из которой вылетает снаряд

    // Параметры стрельбы, которые будут обновляться
    public float attackRate = 0.5f; // Скорость стрельбы (время между выстрелами)
    public int damage = 1; // Скорость стрельбы (время между выстрелами)
    public float damageInterval = 0.5f; // Скорость стрельбы (время между выстрелами)

    private bool isShooting = false; // Флаг, указывающий на состояние стрельбы

    public void LvlUp()
    {
        attackRate--;
        damage++;
        damageInterval /= 1.2f;
    }

    // Сброс флага isShooting при активации объекта
    void OnEnable()
    {
        isShooting = false;
    }

    void Update()
    {
        // Проверка условий для начала стрельбы
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    // Корутина для стрельбы
    IEnumerator Shoot()
    {
        isShooting = true;


        // Создание снаряда в позиции firePoint и с нужным вращением
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, gameObject.transform);
        projectile.GetComponent<PlayerLaser>().Initialize(damage, damageInterval);

        // Задержка перед следующим выстрелом
        yield return new WaitForSeconds(attackRate);

        isShooting = false;
    }

}
