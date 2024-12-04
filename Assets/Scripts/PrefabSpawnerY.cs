using UnityEngine;

public class PrefabSpawnerY : MonoBehaviour
{
    public GameObject initialObject;
    public GameObject prefabToSpawn; // ������ ��� ������
    public GameObject bossPrefab;    // ������ �����
    public int numberOfPrefabs = 10; // ���������� �������� ��� ������
    public float distanceBetweenPrefabs = 5f; // ���������� ����� ��������� �� ��� Y
    
    void Start()
    {
        SpawnPrefabs();
    }

    // ����� ��� ������ �������� ����� �� ��� Y � ���������� �� � �������� �������� ��������
    void SpawnPrefabs()
    {

        // ������� ��������� ������, ���� �� �����
        if (initialObject != null)
        {
            Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject newInitialObject = Instantiate(initialObject, initialPosition, Quaternion.identity, transform);
            newInitialObject.name = initialObject.name + " (Initial)";
        }

        // ���������, ����� �� ������ � ����
        if (prefabToSpawn == null || bossPrefab == null)
        {
            Debug.LogWarning("Prefab or bossPrefab to spawn is not assigned!");
            return;
        }

        // ������� ������� ������� � � ����� ������� �����
        for (int i = 1; i < numberOfPrefabs; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + (i * distanceBetweenPrefabs), transform.position.z);

            // ���� ��� ��������� ������ � ������� �����
            if (i == numberOfPrefabs - 1)
            {
                GameObject boss = Instantiate(bossPrefab, spawnPosition + new Vector3(0, 5), Quaternion.identity, transform);
                boss.name = "Boss";
            }
            else
            {
                GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
                newPrefab.name = prefabToSpawn.name + " " + (i + 1);
            }
        }

        //Debug.Log(numberOfPrefabs + " prefabs spawned as children, including the boss.");
    }
}
