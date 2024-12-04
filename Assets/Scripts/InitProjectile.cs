using UnityEngine;

public class InitProjectile : MonoBehaviour
{
    public PlayerWeaponManager manager;
    public int damage;
    public Vector2 direction; // ����������� ������ �������
    public float moveSpead;

    public void Initialize(PlayerWeaponManager skript,  Vector2 dir, int dmg, float spd)
    {
        manager = skript;
        damage = dmg;
        direction = dir; // ������������� �����������
        moveSpead = spd;
    }
}
