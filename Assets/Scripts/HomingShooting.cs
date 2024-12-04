using System.Collections;
using UnityEngine;

public class HomingShooting : MonoBehaviour
{
    public GameObject homingProjectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка стрельбы
    public float attackRate = 0.5f; // Скорость стрельбы
    public int damage = 1; // Урон от снаряда
    public int bulletsPerShot = 1;

    private Rigidbody2D rb;
    private Coroutine shootingCoroutine; // Ссылка на корутину

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Запускаем корутину, если она еще не запущена
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
    }

    private void OnDisable()
    {
        // Останавливаем корутину и сбрасываем ссылку
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    public void LvlUp()
    {
        bulletsPerShot++;
        damage++;
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                GameObject projectile = Instantiate(homingProjectilePrefab, firePoint.position, firePoint.rotation);
                if (projectile != null)
                {
                    HomingProjectile projectileScript = projectile.GetComponent<HomingProjectile>();
                    projectileScript.Initialize(damage, rb.linearVelocity.y);
                }
                yield return new WaitForSeconds(0.2f);
            }

            // Ждем перед следующим выстрелом
            yield return new WaitForSeconds(attackRate);
        }
    }
}
