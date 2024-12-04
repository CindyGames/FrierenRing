using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObject laserPrefab;  // Префаб лазера
    public Transform firePoint;     // Точка стрельбы (позиция, откуда будет вылетать лазер)
    public float fireRate = 1f;     // Частота стрельбы (время между выстрелами)
    public bool startAttack = false;

    private float timeUntilNextShot = 0f; // Таймер для отслеживания, когда можно стрелять

    private void Start()
    {
        timeUntilNextShot = fireRate;
    }

    void Update()
    {
        // Проверяем, если прошёл таймер до следующего выстрела
        if (timeUntilNextShot <= 0f && startAttack)
        {

            FireLaser();
            timeUntilNextShot = fireRate; // Сбрасываем таймер

        }
        else if (startAttack)
        {
            timeUntilNextShot -= Time.deltaTime; // Отсчитываем время до следующего выстрела
        }
    }

    // Метод для выстрела лазером
    void FireLaser()
    {
        // Создаем лазер на месте точки стрельбы
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        // Делаем лазер дочерним объектом точки стрельбы (чтобы он следовал за ней)
        laser.transform.SetParent(firePoint);

        // Получаем ширину спрайта лазера
        SpriteRenderer laserSprite = laser.GetComponent<SpriteRenderer>();
        if (laserSprite != null)
        {
            // Сдвигаем лазер так, чтобы его левая крайняя точка совпадала с firePoint
            float laserHalfWidth = laserSprite.bounds.size.x / 2;
            laser.transform.localPosition -= new Vector3(laserHalfWidth, 0, 0);
        }
    }
}
