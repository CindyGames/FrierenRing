using UnityEngine;

public class PrefabSpawnerY : MonoBehaviour
{
    public GameObject initialObject;
    public GameObject prefabToSpawn; // Префаб для спавна
    public GameObject bossPrefab;    // Префаб босса
    public int numberOfPrefabs = 10; // Количество префабов для спавна
    public float distanceBetweenPrefabs = 5f; // Расстояние между префабами по оси Y
    
    void Start()
    {
        SpawnPrefabs();
    }

    // Метод для спавна префабов вверх по оси Y и добавления их в качестве дочерних объектов
    void SpawnPrefabs()
    {

        // Спавним начальный объект, если он задан
        if (initialObject != null)
        {
            Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject newInitialObject = Instantiate(initialObject, initialPosition, Quaternion.identity, transform);
            newInitialObject.name = initialObject.name + " (Initial)";
        }

        // Проверяем, задан ли префаб и босс
        if (prefabToSpawn == null || bossPrefab == null)
        {
            Debug.LogWarning("Prefab or bossPrefab to spawn is not assigned!");
            return;
        }

        // Спавним обычные префабы и в конце спавним босса
        for (int i = 1; i < numberOfPrefabs; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + (i * distanceBetweenPrefabs), transform.position.z);

            // Если это последний префаб — спавним босса
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
