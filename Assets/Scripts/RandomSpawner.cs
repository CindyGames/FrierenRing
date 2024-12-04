using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [System.Serializable]  // ����� ������� ��� ��������� � ���������� Unity
    public class SpawnableObject
    {
        public GameObject prefab;     // ������ �������
        [Range(0f, 1f)]
        public float spawnChance;     // ����������� ������ (0.0 - �� ���������, 1.0 - ������ ���������)
    }

    public SpawnableObject[] spawnableObjects;  // ������ �������� � �������������

    void Start()
    {
        TrySpawnObject();
    }

    // ����� ��� ������� ������ �������
    void TrySpawnObject()
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= spawnable.spawnChance)
            {
                SpawnObject(spawnable.prefab);
                return; // ��� ������ ������� ���� ������, ��������� ����
            }
        }

        Debug.Log("No object spawned.");
    }

    // ����� ��� ������ ����������� �������
    void SpawnObject(GameObject objectToSpawn)
    {
        if (objectToSpawn == null)
        {
            Debug.LogWarning("Object to spawn is not assigned!");
            return;
        }

        // ������� ������ �� �����, ��� ��������� ���� ������
        Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform);
        //Debug.Log("Spawned: " + objectToSpawn.name);
    }
}
