using UnityEngine;
using System.Collections;

public class AsteroidPart : Asteroid
{
    private bool destructible;
    [SerializeField] private float destructibleDelayTime = 1f;
    public override void Awake()
    {
        base.Awake();
        StartCoroutine(DestructibleDelay(destructibleDelayTime));
    }

    public override void DestroyObjectOnScene()
    {
        EventBus.OnEnemyDestroyed(this.gameObject, 50);
        ParticleSystem vfx = Instantiate(GetAsteroidDestroyPrefab(), this.transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(this.gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("PlayerWeapon") || collision.CompareTag("Player")) && destructible)
        {
            float damage = collision.gameObject.GetComponent<ObjectOnScene>().CollideDamage();
            GetDamage(damage);
        }
    }

    IEnumerator DestructibleDelay(float destructibleDelayTime)
    {
        yield return new WaitForSeconds(destructibleDelayTime);
        destructible = true;
    }
}
