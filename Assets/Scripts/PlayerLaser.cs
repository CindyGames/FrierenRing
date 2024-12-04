using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public float laserDuration = 2f; // ����� ������������� ������
    private int laserDamage = 1; // ����, ������� ������� �����

    public Vector3 laserStartScale = new Vector3(1f, 0f, 1f); // ��������� ������� ������ (���� �� Y)
    public Vector3 laserEndScale = new Vector3(1f, 5f, 1f); // �������� ������� ������
    public float growSpeed = 5f; // �������� ����� ������
    public float damageInterval = 0.5f; // �������� ����� ���������� �����
    private SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer

    private List<EnemyStats> enemiesInLaser = new List<EnemyStats>(); // ������ ������ � ���� ������
    private List<BossStats2> bossesInLaser = new List<BossStats2>(); // ������ ������ � ���� ������

    public void Initialize(int dmg, float damageInter)
    {
        laserDamage = dmg;
        damageInterval = damageInter;
    }

    void Start()
    {

        // �������� ��������� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ������������� ������ ��������� ������ (���� �� Y)
        transform.localScale = laserStartScale;

        // ��������� ������ ��� ����������� ������ ����� ��������� ������
        Invoke("DestroyLaser", laserDuration);

        // ��������� �������� ��� �������������� ��������� �����
        StartCoroutine(DealDamageOverTime());
    }

    void Update()
    {
        // ���������� ����������� ������ ������ �� ������� ��������
        if (transform.localScale.y < laserEndScale.y)
        {
            float newScaleY = Mathf.MoveTowards(transform.localScale.y, laserEndScale.y, growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(laserEndScale.x, newScaleY, laserEndScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null && !enemiesInLaser.Contains(enemyStats))
            {
                enemiesInLaser.Add(enemyStats); // ��������� ����� � ������
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null && !bossesInLaser.Contains(bossStats))
            {
                bossesInLaser.Add(bossStats); // ��������� ����� � ������
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemiesInLaser.Remove(enemyStats); // ������� ����� �� ������
            }

            BossStats2 bossStats = other.GetComponentInChildren<BossStats2>();
            if (bossStats != null)
            {
                bossesInLaser.Remove(bossStats); // ������� ����� �� ������
            }
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            // ������� ���� ���� ������ � ����
            for (int i = enemiesInLaser.Count - 1; i >= 0; i--)
            {
                if (enemiesInLaser[i] != null)
                {
                    enemiesInLaser[i].TakeDamage(laserDamage);
                }
                else
                {
                    enemiesInLaser.RemoveAt(i);
                }
            }

            // ������� ���� ���� ������ � ����
            for (int i = bossesInLaser.Count - 1; i >= 0; i--)
            {
                if (bossesInLaser[i] != null)
                {
                    bossesInLaser[i].TakeDamage(laserDamage);
                }
                else
                {
                    bossesInLaser.RemoveAt(i);
                }
            }

            // ���� �������� ����� ��������� ���������� �����
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // ���������� ����� ����� �����
    private void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
