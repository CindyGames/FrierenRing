using UnityEngine;

public class BossZoneTrigger2D : MonoBehaviour
{
    public OptimizedSpawner spawner; // ������ �� ������ ��������
    public Vector3 addVector;

    private void Start()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null ) 
        {
        transform.position = boss.transform.position + addVector;
        }
    }

    // ���������, ������ �� ����� � ���� �����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���� ����� ����� ��� "Player"
        {
            // ��������� ����� ��������
            spawner.isInBossZone = true;
            Debug.Log("Player entered the boss zone. Spawning disabled.");
        }
    }

    // ���������, ������� �� ����� �� ���� �����
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���� ����� ����� ��� "Player"
        {
            // �������� ����� ��������
            spawner.isInBossZone = false;
            Debug.Log("Player exited the boss zone. Spawning enabled.");
        }
    }
}
