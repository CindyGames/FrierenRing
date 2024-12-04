using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [System.Serializable]  // Чтобы увидеть эту структуру в инспекторе Unity
    public class SpawnableObject
    {
        public GameObject prefab;     // Префаб объекта
        [Range(0f, 1f)]
        public float spawnChance;     // Вероятность спавна (0.0 - не спавнится, 1.0 - всегда спавнится)
    }

    public SpawnableObject[] spawnableObjects;  // Массив объектов с вероятностями

    void Start()
    {
        TrySpawnObject();
    }

    // Метод для попытки спавна объекта
    void TrySpawnObject()
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= spawnable.spawnChance)
            {
                SpawnObject(spawnable.prefab);
                return; // Как только спавним один объект, завершаем цикл
            }
        }

        Debug.Log("No object spawned.");
    }

    // Метод для спавна конкретного объекта
    void SpawnObject(GameObject objectToSpawn)
    {
        if (objectToSpawn == null)
        {
            Debug.LogWarning("Object to spawn is not assigned!");
            return;
        }

        // Спавним объект на месте, где находится этот скрипт
        Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform);
        //Debug.Log("Spawned: " + objectToSpawn.name);
    }
}
