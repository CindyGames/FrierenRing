using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ������ �������� ��������

    void Start()
    {
        SpawnRandomMonster();
    }

    // ����� ��� ������ ���������� �������
    void SpawnRandomMonster()
    {
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogWarning("No monsters to spawn!");
            return;
        }

        // �������� ���������� ������� �� ������
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monsterToSpawn = monsterPrefabs[randomIndex];

        // ������� ������� �� �����, ��� ��������� ���� ������
        Instantiate(monsterToSpawn, transform.position, Quaternion.identity, transform);
        //Debug.Log("Spawned: " + monsterToSpawn.name);
    }
}
