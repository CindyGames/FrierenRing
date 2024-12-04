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

    public SpawnableObject[] spawnableObjects;  // Массив объектов с вероятностями

    public float doubleSpawnChance = 0.3f;      // Вероятность спавна двух объектов по краям
    public Vector2 leftSpawnOffset = new Vector2(-2.5f, 0);
    public Vector2 rightSpawnOffset = new Vector2(2.5f, 0);
    public Vector2 middleSpawnOffset = new Vector2(0f, 0);

    public float spawnDistance = 5f;            // Дистанция между генерацией новых объектов
    public float despawnDistance = 10f;         // Дистанция для удаления объектов под экраном
    public int preSpawnAhead = 3;               // Количество объектов, спавнимых над экраном заранее

    private float lastSpawnY = 0f;              // Последняя позиция Y, где был спавн объектов
    private Transform playerTransform;           // Ссылка на трансформ игрока
    public List<GameObject> activeObjects = new List<GameObject>();  // Список активных объектов
    public bool isInBossZone = false;           // Флаг для отслеживания, находится ли игрок в зоне босса

    public bool spawnBack = false;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Спавним начальные объекты
        for (int i = 0; i < preSpawnAhead; i++)
        {
            SpawnNextObject();
        }
    }

    void Update()
    {
        // Проверяем, если игрок прошел на spawnDistance вперед, то создаем новые объекты
        if (playerTransform.position.y - lastSpawnY > spawnDistance)
        {
            SpawnNextObject();
        }

        // Удаляем объекты, которые ушли за пределы экрана
        DespawnOldObjects();
    }

    // Метод для очистки списка от несуществующих объектов
    void CleanupNullObjects()
    {
        activeObjects.RemoveAll(obj => obj == null);
    }

    // Метод для спавна следующего объекта
    void SpawnNextObject()
    {
        // Определяем высоту спавна на основе последнего спавна
        lastSpawnY += spawnDistance;

        // Спавним объекты, если не находимся в зоне босса
        if (!isInBossZone)
        {
            // Смещение для спавна
            SpawnObjectAtOffset(lastSpawnY);
        }
    }

    // Метод для спавна объектов с учетом вероятности
    void SpawnObjectAtOffset(float yOffset)
    {
        float randomDoubleSpawn = Random.Range(0f, 1f);

        // Устанавливаем смещение Y для избежания спавна над экраном
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

    // Спавн объектов по краям
    void SpawnObjectsOnEdges(float yOffset)
    {
        SpawnableObject leftSpawnable = GetRandomSpawnableObject();
        if (leftSpawnable != null)
        {
            // Спавним объект за экраном
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

    // Спавн объекта в центре
    void SpawnObjectInMiddle(float yOffset)
    {
        SpawnableObject middleSpawnable = GetRandomSpawnableObject();
        if (middleSpawnable != null)
        {
            var middleObj = Instantiate(middleSpawnable.prefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
            activeObjects.Add(middleObj);
        }
    }

    // Метод для выбора случайного объекта на основе вероятности
    SpawnableObject GetRandomSpawnableObject()
    {
        float totalChance = 0f;

        // Считаем общую вероятность
        foreach (var spawnable in spawnableObjects)
        {
            totalChance += spawnable.spawnChance;
        }

        // Генерируем случайное число от 0 до общей вероятности
        float randomValue = Random.Range(0f, totalChance);

        // Выбираем объект
        foreach (var spawnable in spawnableObjects)
        {
            randomValue -= spawnable.spawnChance;
            if (randomValue <= 0f)
            {
                return spawnable; // Возвращаем выбранный объект
            }
        }

        return null; // Возвращаем null, если ничего не выбрано
    }

    // Метод для удаления объектов, которые находятся под экраном
    void DespawnOldObjects()
    {
        // Очищаем список от несуществующих объектов
        CleanupNullObjects();

        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            // Проверяем, существует ли объект и находится ли он под экраном
            if (activeObjects[i] != null && activeObjects[i].transform.position.y < playerTransform.position.y - despawnDistance)
            {
                Destroy(activeObjects[i]);
                activeObjects.RemoveAt(i);
            }
        }
    }
}
