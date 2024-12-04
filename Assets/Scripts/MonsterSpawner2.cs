using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner2 : MonoBehaviour
{

    public List<GameObject> monsters; // ������ ���� ��������� ��������
    public float healthMultiplier = 1.2f; // ����� ��������� ��������

    void Start()
    {
        SpawnRandomMonster();
    }

    void SpawnRandomMonster()
    {
        if (monsters.Count == 0)
        {
            Debug.LogWarning("������ �������� ����, ����� ����������!");
            return;
        }

        // �������� ���������� �������
        int randomIndex = Random.Range(0, monsters.Count);

        // ������� ������� � ������� �������
        GameObject monsterInstance = Instantiate(monsters[randomIndex], transform.position, Quaternion.identity, gameObject.transform);

        // ������� ��������� MonsterHealth ��� ���������� ���������
        EnemyStats healthComponent = monsterInstance.GetComponent<EnemyStats>();
        if (healthComponent != null)
        {
            healthComponent.GetHp();
            // ����������� �������� �� ���������
            healthComponent.power = (int)(healthComponent.power  * healthMultiplier);
        }
        else
        {
            Debug.LogWarning("��������� MonsterHealth �� ������ �� ������� " + monsters[randomIndex].name);
        }
    }
}
