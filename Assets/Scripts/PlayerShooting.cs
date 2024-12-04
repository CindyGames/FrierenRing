using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка, из которой вылетает снаряд

    // Параметры стрельбы, которые будут обновляться
    public float attackRate = 0.5f; // Скорость стрельбы (время между выстрелами)
    public int damage = 2; // Урон от одного снаряда
    public int bulletsPerShot = 3; // Количество пуль, вылетающих одновременно

    public float maxSpreadAngle = 10f; // Максимальный угол разброса пуль
    private bool isShooting = false; // Флаг, указывающий на состояние стрельбы


    private PlayerWeaponManager weaponManager; // Ссылка на менеджер оружия
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        weaponManager = GetComponentInParent<PlayerWeaponManager>(); // Найти компонент PlayerWeaponManager
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

        // Запуск пуль в количестве bulletsPerShot
        for (int i = 0; i < bulletsPerShot + weaponManager.bulletsPerShot; i++)
        {
            // Генерация случайного угла разброса
            float angle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);

            // Создание снаряда в позиции firePoint и с нужным вращением
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
            InitProjectile projectileScript = projectile.GetComponent<InitProjectile>();

            // Передача параметров снаряду
            Vector2 shootDirection = rotation * Vector2.right;
            projectileScript.Initialize(weaponManager, shootDirection, damage + weaponManager.damage, rb.linearVelocityY);

            yield return new WaitForSeconds(0.1f);

        }

        // Задержка перед следующим выстрелом
        yield return new WaitForSeconds(attackRate / weaponManager.attackRate);

        isShooting = false;
    }

}
