using UnityEngine;

public class LaserWeapon : Weapon
{
    [SerializeField] private float damage     = 10;
    [SerializeField] private float reloadTime = 10f;
    [SerializeField] private float maxAmmo    = 3f;
    [SerializeField] private GameObject laserBulletPrefab;

    private void Awake()
    {
        InitWeapon(damage, reloadTime, maxAmmo, this.gameObject);
    }

    public override void AttackWeapon()
    {
         base.AttackWeapon();
         GameObject laserBullet = Instantiate(laserBulletPrefab, this.transform.position, Quaternion.identity);
         laserBullet.GetComponent<LaserBullet>().InitLaserBullet(this.transform, damage); 
    }
}
