using UnityEngine;

public class CombinedSpawner : MonoBehaviour
{
    [System.Serializable]  // ����� ������� ��� ��������� � ���������� Unity
    public class SpawnableObject
    {
        public GameObject prefab;     // ������ �������
        [Range(0f, 1f)]
        public float spawnChance;     // ����������� ������ (0.0 - �� ���������, 1.0 - ������ ���������)
    }

    public SpawnableObject[] spawnableObjects;  // ������ �������� � �������������
    public GameObject firstPrefab;              // ������ ������
    public GameObject lastPrefab;               // ��������� ������
    public GameObject lastSpawner;
    public GameObject WinScreen; // hold linl for boss

    public float doubleSpawnChance = 0.3f;      // ����������� ������ ���� �������� �� �����
    public Vector2 leftSpawnOffset = new Vector2(-2.5f, 0); // �������� ��� ������ ����
    public Vector2 rightSpawnOffset = new Vector2(2.5f, 0); // �������� ��� ������� ����
    public Vector2 middleSpawnOffset = new Vector2(0f, 0);  // �������� ��� ������

    public int numberOfRepeats = 5;    // ���������� �������� ������
    public float ySpacing = 5f;        // ���������� ����� �������� �� ��� Y

    void Start()
    {
        RepeatSpawning();  // ��������� ������������� ����� ��������
    }

    // ����� ��� �������������� ������ ��������
    void RepeatSpawning()
    {
        for (int i = 0; i < numberOfRepeats; i++)
        {
            float yOffset = i * ySpacing;  // ��������� �������� �� ��� Y ��� ������� ������

            if (i == 0)
            {
                // ������� ������ ������
                SpawnFirstPrefab(yOffset);
            }
            else if (i == numberOfRepeats - 1)
            {
                // ������� ��������� ������
                SpawnLastPrefab(yOffset);
            }
            else
            {
                // ������� ������� ������� � ������������ ������
                TrySpawnObject(yOffset);
            }
        }
    }

    // ����� ��� ������ ������� �������
    void SpawnFirstPrefab(float yOffset)
    {
        if (firstPrefab != null)
        {
            Instantiate(firstPrefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("First prefab is not assigned!");
        }
    }

    // ����� ��� ������ ���������� �������
    void SpawnLastPrefab(float yOffset)
    {
        if (lastPrefab != null)
        {
            Instantiate(lastPrefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset + ySpacing)), Quaternion.identity, transform);
            Instantiate(lastSpawner, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset + ySpacing)), Quaternion.identity, transform);

        }
        else
        {
            Debug.LogWarning("Last prefab is not assigned!");
        }
    }

    // ����� ��� ������� ������ �������� � ������ �������� �� Y
    void TrySpawnObject(float yOffset)
    {
        // ���������, ����� �� ���������� ��� ������� �� ����� ������
        float randomDoubleSpawn = Random.Range(0f, 1f);

        if (randomDoubleSpawn <= doubleSpawnChance)
        {
            // ������� ��� ������� �� �����
            SpawnObjectsOnEdges(yOffset);
        }
        else
        {
            // ������� ���� ������ � ������
            SpawnObjectInMiddle(yOffset);
        }
    }

    // ����� ��� ������ ���� �������� �� ����� ������ � ������ �������� �� ��� Y
    void SpawnObjectsOnEdges(float yOffset)
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= spawnable.spawnChance)
            {
                // ������� �� ����� ���� ������������ ������� ��������
                Instantiate(spawnable.prefab, transform.position + (Vector3)(leftSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);

                // ������� �� ������ ���� ������������ ������� ��������
                Instantiate(spawnable.prefab, transform.position + (Vector3)(rightSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
                return; // ������������� ����� ������
            }
        }

        Debug.Log("No objects spawned on edges.");
    }

    // ����� ��� ������ ������ ������� � ������ ������ � ������ �������� �� ��� Y
    void SpawnObjectInMiddle(float yOffset)
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= spawnable.spawnChance)
            {
                // ������� � ������ ������������ ������� ��������
                Instantiate(spawnable.prefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
                return; // ������������� ����� ������
            }
        }

        Debug.Log("No object spawned in the middle.");
    }
}
