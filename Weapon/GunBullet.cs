using System.Collections;
using UnityEngine;

public class GunBullet : ObjectOnScene
{
    private float   moveSpeed;
    private Vector3 direction;
    private float   damage;
    private float   bulletLifetime;

    private void Awake()
    {
        InitObjectOnScene();
    }

    void Update()
    {
        Move(direction * moveSpeed * Time.deltaTime);
        LoopCheckPosition();
    }

    public void InitGunBullet(Vector3 direction, float moveSpeed, float damage, float bulletLifetime)
    {
        this.direction  = direction;
        this.moveSpeed  = moveSpeed;
        this.damage     = damage;
        this.bulletLifetime = bulletLifetime;
        StartCoroutine(LifetimeDelay(bulletLifetime));
    }

    public override float CollideDamage()
    {
        DestroyObjectOnScene();
        return damage;
    }

    IEnumerator LifetimeDelay(float bulletLifetime)
    {
        yield return new WaitForSeconds(bulletLifetime);
        DestroyObjectOnScene();
    }

    public override void DestroyObjectOnScene()
    {
        Destroy(this.gameObject);
    }
}
