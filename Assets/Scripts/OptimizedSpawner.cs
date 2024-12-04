using UnityEngine;
using System.Collections.Generic;

public class OptimizedSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnChance;
    }

    public SpawnableObject[] spawnableObjects;  // ������ �������� � �������������

    public float doubleSpawnChance = 0.3f;      // ����������� ������ ���� �������� �� �����
    public Vector2 leftSpawnOffset = new Vector2(-2.5f, 0);
    public Vector2 rightSpawnOffset = new Vector2(2.5f, 0);
    public Vector2 middleSpawnOffset = new Vector2(0f, 0);

    public float spawnDistance = 5f;            // ��������� ����� ���������� ����� ��������
    public float despawnDistance = 10f;         // ��������� ��� �������� �������� ��� �������
    public int preSpawnAhead = 3;               // ���������� ��������, ��������� ��� ������� �������

    private float lastSpawnY = 0f;              // ��������� ������� Y, ��� ��� ����� ��������
    private Transform playerTransform;           // ������ �� ��������� ������
    public List<GameObject> activeObjects = new List<GameObject>();  // ������ �������� ��������
    public bool isInBossZone = false;           // ���� ��� ������������, ��������� �� ����� � ���� �����

    public bool spawnBack = false;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // ������� ��������� �������
        for (int i = 0; i < preSpawnAhead; i++)
        {
            SpawnNextObject();
        }
    }

    void Update()
    {
        // ���������, ���� ����� ������ �� spawnDistance ������, �� ������� ����� �������
        if (playerTransform.position.y - lastSpawnY > spawnDistance)
        {
            SpawnNextObject();
        }

        // ������� �������, ������� ���� �� ������� ������
        DespawnOldObjects();
    }

    // ����� ��� ������� ������ �� �������������� ��������
    void CleanupNullObjects()
    {
        activeObjects.RemoveAll(obj => obj == null);
    }

    // ����� ��� ������ ���������� �������
    void SpawnNextObject()
    {
        // ���������� ������ ������ �� ������ ���������� ������
        lastSpawnY += spawnDistance;

        // ������� �������, ���� �� ��������� � ���� �����
        if (!isInBossZone)
        {
            // �������� ��� ������
            SpawnObjectAtOffset(lastSpawnY);
        }
    }

    // ����� ��� ������ �������� � ������ �����������
    void SpawnObjectAtOffset(float yOffset)
    {
        float randomDoubleSpawn = Random.Range(0f, 1f);

        // ������������� �������� Y ��� ��������� ������ ��� �������
        yOffset += despawnDistance;

        if (randomDoubleSpawn <= doubleSpawnChance)
        {
            SpawnObjectsOnEdges(yOffset);
        }
        else
        {
            SpawnObjectInMiddle(yOffset);
        }
    }

    // ����� �������� �� �����
    void SpawnObjectsOnEdges(float yOffset)
    {
        SpawnableObject leftSpawnable = GetRandomSpawnableObject();
        if (leftSpawnable != null)
        {
            // ������� ������ �� �������
            var leftObj = Instantiate(leftSpawnable.prefab, transform.position + (Vector3)(leftSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
            activeObjects.Add(leftObj);
        }

        SpawnableObject rightSpawnable = GetRandomSpawnableObject();
        if (rightSpawnable != null)
        {
            var rightObj = Instantiate(rightSpawnable.prefab, transform.position + (Vector3)(rightSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
            activeObjects.Add(rightObj);
        }
    }

    // ����� ������� � ������
    void SpawnObjectInMiddle(float yOffset)
    {
        SpawnableObject middleSpawnable = GetRandomSpawnableObject();
        if (middleSpawnable != null)
        {
            var middleObj = Instantiate(middleSpawnable.prefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
            activeObjects.Add(middleObj);
        }
    }

    // ����� ��� ������ ���������� ������� �� ������ �����������
    SpawnableObject GetRandomSpawnableObject()
    {
        float totalChance = 0f;

        // ������� ����� �����������
        foreach (var spawnable in spawnableObjects)
        {
            totalChance += spawnable.spawnChance;
        }

        // ���������� ��������� ����� �� 0 �� ����� �����������
        float randomValue = Random.Range(0f, totalChance);

        // �������� ������
        foreach (var spawnable in spawnableObjects)
        {
            randomValue -= spawnable.spawnChance;
            if (randomValue <= 0f)
            {
                return spawnable; // ���������� ��������� ������
            }
        }

        return null; // ���������� null, ���� ������ �� �������
    }

    // ����� ��� �������� ��������, ������� ��������� ��� �������
    void DespawnOldObjects()
    {
        // ������� ������ �� �������������� ��������
        CleanupNullObjects();

        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            // ���������, ���������� �� ������ � ��������� �� �� ��� �������
            if (activeObjects[i] != null && activeObjects[i].transform.position.y < playerTransform.position.y - despawnDistance)
            {
                Destroy(activeObjects[i]);
                activeObjects.RemoveAt(i);
            }
        }
    }
}
