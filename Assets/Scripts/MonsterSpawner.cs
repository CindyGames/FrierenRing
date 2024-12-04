using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // Список префабов монстров

    void Start()
    {
        SpawnRandomMonster();
    }

    // Метод для спавна случайного монстра
    void SpawnRandomMonster()
    {
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogWarning("No monsters to spawn!");
            return;
        }

        // Выбираем случайного монстра из списка
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monsterToSpawn = monsterPrefabs[randomIndex];

        // Спавним монстра на месте, где находится этот скрипт
        Instantiate(monsterToSpawn, transform.position, Quaternion.identity, transform);
        //Debug.Log("Spawned: " + monsterToSpawn.name);
    }
}
