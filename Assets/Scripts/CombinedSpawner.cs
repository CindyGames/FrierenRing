using UnityEngine;

public class CombinedSpawner : MonoBehaviour
{
    [System.Serializable]  // Чтобы увидеть эту структуру в инспекторе Unity
    public class SpawnableObject
    {
        public GameObject prefab;     // Префаб объекта
        [Range(0f, 1f)]
        public float spawnChance;     // Вероятность спавна (0.0 - не спавнится, 1.0 - всегда спавнится)
    }

    public SpawnableObject[] spawnableObjects;  // Массив объектов с вероятностями
    public GameObject firstPrefab;              // Первый префаб
    public GameObject lastPrefab;               // Последний префаб
    public GameObject lastSpawner;
    public GameObject WinScreen; // hold linl for boss

    public float doubleSpawnChance = 0.3f;      // Вероятность спавна двух объектов по краям
    public Vector2 leftSpawnOffset = new Vector2(-2.5f, 0); // Смещение для левого края
    public Vector2 rightSpawnOffset = new Vector2(2.5f, 0); // Смещение для правого края
    public Vector2 middleSpawnOffset = new Vector2(0f, 0);  // Смещение для центра

    public int numberOfRepeats = 5;    // Количество повторов спавна
    public float ySpacing = 5f;        // Расстояние между спавнами по оси Y

    void Start()
    {
        RepeatSpawning();  // Запускаем повторяющийся спавн объектов
    }

    // Метод для повторяющегося спавна объектов
    void RepeatSpawning()
    {
        for (int i = 0; i < numberOfRepeats; i++)
        {
            float yOffset = i * ySpacing;  // Вычисляем смещение по оси Y для каждого спавна

            if (i == 0)
            {
                // Спавним первый префаб
                SpawnFirstPrefab(yOffset);
            }
            else if (i == numberOfRepeats - 1)
            {
                // Спавним последний префаб
                SpawnLastPrefab(yOffset);
            }
            else
            {
                // Спавним обычные объекты с вероятностью спавна
                TrySpawnObject(yOffset);
            }
        }
    }

    // Метод для спавна первого префаба
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

    // Метод для спавна последнего префаба
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

    // Метод для попытки спавна объектов с учетом смещения по Y
    void TrySpawnObject(float yOffset)
    {
        // Проверяем, будет ли спавниться два объекта по краям дороги
        float randomDoubleSpawn = Random.Range(0f, 1f);

        if (randomDoubleSpawn <= doubleSpawnChance)
        {
            // Спавним два объекта по краям
            SpawnObjectsOnEdges(yOffset);
        }
        else
        {
            // Спавним один объект в центре
            SpawnObjectInMiddle(yOffset);
        }
    }

    // Метод для спавна двух объектов по краям дороги с учетом смещения по оси Y
    void SpawnObjectsOnEdges(float yOffset)
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= spawnable.spawnChance)
            {
                // Спавним на левом краю относительно позиции спавнера
                Instantiate(spawnable.prefab, transform.position + (Vector3)(leftSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);

                // Спавним на правом краю относительно позиции спавнера
                Instantiate(spawnable.prefab, transform.position + (Vector3)(rightSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
                return; // Останавливаем после спавна
            }
        }

        Debug.Log("No objects spawned on edges.");
    }

    // Метод для спавна одного объекта в центре дороги с учетом смещения по оси Y
    void SpawnObjectInMiddle(float yOffset)
    {
        foreach (var spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= spawnable.spawnChance)
            {
                // Спавним в центре относительно позиции спавнера
                Instantiate(spawnable.prefab, transform.position + (Vector3)(middleSpawnOffset + new Vector2(0, yOffset)), Quaternion.identity, transform);
                return; // Останавливаем после спавна
            }
        }

        Debug.Log("No object spawned in the middle.");
    }
}
