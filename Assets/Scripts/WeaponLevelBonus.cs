using UnityEngine;

public class WeaponLevelBonus : MonoBehaviour
{
    public enum WeaponType
    {
        laser,
        rocket
    }
    public WeaponType type;

    PlayerWeaponManager playerWeaponManager;
    private void Start()
    {
        playerWeaponManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponManager>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка, если бонус собирает игрок
        if (other.CompareTag("Player"))
        {

            ApplyUpgrade();

            //Destroy(gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    public void ApplyUpgrade()
    {
            switch (type)
            {
                case WeaponType.laser:
                    if (playerWeaponManager.laserShooter.gameObject.activeSelf == false)
                    {
                        playerWeaponManager.laserShooter.gameObject.SetActive(true);
                    }
                    else
                    {
                        playerWeaponManager.laserShooter.LvlUp();
                    }
                    break;

                case WeaponType.rocket:
                    if (playerWeaponManager.rocket.gameObject.activeSelf == false)
                    {
                        playerWeaponManager.rocket.gameObject.SetActive(true);
                    }
                    else
                    {
                        playerWeaponManager.rocket.LvlUp();
                    }
                    break;

                default:
                    break;
            }
        GetComponent<AudioSource>().Play();
    }

}
