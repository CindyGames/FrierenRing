using UnityEngine;

public class InitProjectile : MonoBehaviour
{
    public PlayerWeaponManager manager;
    public int damage;
    public Vector2 direction; // Направление полета снаряда
    public float moveSpead;

    public void Initialize(PlayerWeaponManager skript,  Vector2 dir, int dmg, float spd)
    {
        manager = skript;
        damage = dmg;
        direction = dir; // Устанавливаем направление
        moveSpead = spd;
    }
}
