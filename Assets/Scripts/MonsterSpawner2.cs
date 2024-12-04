using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner2 : MonoBehaviour
{

    public List<GameObject> monsters; // Список всех доступных монстров
    public float healthMultiplier = 1.2f; // Общий множитель здоровья

    void Start()
    {
        SpawnRandomMonster();
    }

    void SpawnRandomMonster()
    {
        if (monsters.Count == 0)
        {
            Debug.LogWarning("Список монстров пуст, спавн невозможен!");
            return;
        }

        // Выбираем случайного монстра
        int randomIndex = Random.Range(0, monsters.Count);

        // Спавним монстра в текущей позиции
        GameObject monsterInstance = Instantiate(monsters[randomIndex], transform.position, Quaternion.identity, gameObject.transform);

        // Находим компонент MonsterHealth для управления здоровьем
        EnemyStats healthComponent = monsterInstance.GetComponent<EnemyStats>();
        if (healthComponent != null)
        {
            healthComponent.GetHp();
            // Увеличиваем здоровье на множитель
            healthComponent.power = (int)(healthComponent.power  * healthMultiplier);
        }
        else
        {
            Debug.LogWarning("Компонент MonsterHealth не найден на префабе " + monsters[randomIndex].name);
        }
    }
}
