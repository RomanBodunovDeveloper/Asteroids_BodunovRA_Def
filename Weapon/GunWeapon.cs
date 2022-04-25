using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField] private float damage         = 1;
    [SerializeField] private float reloadTime     = 0.3f;
    [SerializeField] private float bulletSpeed    = 3f;
    [SerializeField] private float bulletLifetime = 5f;
    [SerializeField] private float maxAmmo        = 1f;
    [SerializeField] private GameObject gunBulletPrefab;
 
    private void Awake()
    {
        InitWeapon(damage, reloadTime, maxAmmo, this.gameObject);
    }

    public override void AttackWeapon()
    {
        base.AttackWeapon();
        GameObject gunBullet = Instantiate(gunBulletPrefab, this.transform.position, Quaternion.identity);
        gunBullet.GetComponent<GunBullet>().InitGunBullet(this.transform.up, bulletSpeed, damage, bulletLifetime);
    }
}
