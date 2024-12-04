using UnityEngine;

public class BossZoneTrigger2D : MonoBehaviour
{
    public OptimizedSpawner spawner; // Ссылка на скрипт спавнера
    public Vector3 addVector;

    private void Start()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null ) 
        {
        transform.position = boss.transform.position + addVector;
        }
    }

    // Проверяем, входит ли игрок в зону босса
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Если игрок имеет тэг "Player"
        {
            // Отключаем спавн объектов
            spawner.isInBossZone = true;
            Debug.Log("Player entered the boss zone. Spawning disabled.");
        }
    }

    // Проверяем, выходит ли игрок из зоны босса
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Если игрок имеет тэг "Player"
        {
            // Включаем спавн объектов
            spawner.isInBossZone = false;
            Debug.Log("Player exited the boss zone. Spawning enabled.");
        }
    }
}
