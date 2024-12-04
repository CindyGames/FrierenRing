using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObject laserPrefab;  // ������ ������
    public Transform firePoint;     // ����� �������� (�������, ������ ����� �������� �����)
    public float fireRate = 1f;     // ������� �������� (����� ����� ����������)
    public bool startAttack = false;

    private float timeUntilNextShot = 0f; // ������ ��� ������������, ����� ����� ��������

    private void Start()
    {
        timeUntilNextShot = fireRate;
    }

    void Update()
    {
        // ���������, ���� ������ ������ �� ���������� ��������
        if (timeUntilNextShot <= 0f && startAttack)
        {

            FireLaser();
            timeUntilNextShot = fireRate; // ���������� ������

        }
        else if (startAttack)
        {
            timeUntilNextShot -= Time.deltaTime; // ����������� ����� �� ���������� ��������
        }
    }

    // ����� ��� �������� �������
    void FireLaser()
    {
        // ������� ����� �� ����� ����� ��������
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        // ������ ����� �������� �������� ����� �������� (����� �� �������� �� ���)
        laser.transform.SetParent(firePoint);

        // �������� ������ ������� ������
        SpriteRenderer laserSprite = laser.GetComponent<SpriteRenderer>();
        if (laserSprite != null)
        {
            // �������� ����� ���, ����� ��� ����� ������� ����� ��������� � firePoint
            float laserHalfWidth = laserSprite.bounds.size.x / 2;
            laser.transform.localPosition -= new Vector3(laserHalfWidth, 0, 0);
        }
    }
}
