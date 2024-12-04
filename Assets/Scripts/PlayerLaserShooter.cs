using System.Collections;
using UnityEngine;

public class PlayerLaserShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint; // �����, �� ������� �������� ������

    // ��������� ��������, ������� ����� �����������
    public float attackRate = 0.5f; // �������� �������� (����� ����� ����������)
    public int damage = 1; // �������� �������� (����� ����� ����������)
    public float damageInterval = 0.5f; // �������� �������� (����� ����� ����������)

    private bool isShooting = false; // ����, ����������� �� ��������� ��������

    public void LvlUp()
    {
        attackRate--;
        damage++;
        damageInterval /= 1.2f;
    }

    // ����� ����� isShooting ��� ��������� �������
    void OnEnable()
    {
        isShooting = false;
    }

    void Update()
    {
        // �������� ������� ��� ������ ��������
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    // �������� ��� ��������
    IEnumerator Shoot()
    {
        isShooting = true;


        // �������� ������� � ������� firePoint � � ������ ���������
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, gameObject.transform);
        projectile.GetComponent<PlayerLaser>().Initialize(damage, damageInterval);

        // �������� ����� ��������� ���������
        yield return new WaitForSeconds(attackRate);

        isShooting = false;
    }

}
