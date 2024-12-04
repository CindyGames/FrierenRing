using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject projectilePrefab;  // ������ ������� (����)
    public List<Transform> attackPoints; // ������ �����, ������ ����� ����������� ����
    public float attackInterval = 2f;    // �������� ����� ������� (� ��������)

    void Start()
    {
        if (attackPoints.Count == 0)
        {
            Debug.LogWarning("�� ��������� ����� �����!");
        }
    }
    public void StartAttack()
    {
        // �������� �������� �����
        StartCoroutine(AttackRoutine());
    }
    // ������� ��� ���������� ����� � ����������
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

    // ����� ��� �����, ������� ������ � ��������� �����
    void Attack()
    {
        // �������� ��������� ����� �� ������
        int randomIndex = Random.Range(0, attackPoints.Count);
        Transform selectedAttackPoint = attackPoints[randomIndex];

        // ������� ������
        GameObject projectile = Instantiate(projectilePrefab, selectedAttackPoint.position, selectedAttackPoint.rotation);

    }

    // ������������ ����� ����� � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        foreach (Transform point in attackPoints)
        {
            if (point != null)
            {
                Gizmos.DrawSphere(point.position, 0.3f); // ���������� ����� ��� �����
            }
        }
    }
}
