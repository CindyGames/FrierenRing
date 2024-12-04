using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject projectilePrefab;  // Префаб снаряда (шара)
    public List<Transform> attackPoints; // Список точек, откуда будут создаваться шары
    public float attackInterval = 2f;    // Интервал между атаками (в секундах)

    void Start()
    {
        if (attackPoints.Count == 0)
        {
            Debug.LogWarning("Не назначены точки атаки!");
        }
    }
    public void StartAttack()
    {
        // Начинаем корутину атаки
        StartCoroutine(AttackRoutine());
    }
    // Корутин для выполнения атаки с интервалом
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (attackPoints.Count > 0)
            {
                Attack();
            }
        }
    }

    // Метод для атаки, создает снаряд в случайной точке
    void Attack()
    {
        // Выбираем случайную точку из списка
        int randomIndex = Random.Range(0, attackPoints.Count);
        Transform selectedAttackPoint = attackPoints[randomIndex];

        // Создаем снаряд
        GameObject projectile = Instantiate(projectilePrefab, selectedAttackPoint.position, selectedAttackPoint.rotation);

    }

    // Визуализация точек атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        foreach (Transform point in attackPoints)
        {
            if (point != null)
            {
                Gizmos.DrawSphere(point.position, 0.3f); // Отображаем точки как сферы
            }
        }
    }
}
