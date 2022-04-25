using UnityEngine;

public class Ufo : Character
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float health    = 2f;
    [SerializeField] private float damage    = 1f;
    [SerializeField] private ParticleSystem ufoDestroyPrefab;

    private Vector3 direction;

    public override void OnEnable()
    {
        base.OnEnable();
        EventBus.StarshipNavigationChange += CalcDirection;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventBus.StarshipNavigationChange -= CalcDirection;
    }

    public virtual void Awake()
    {
        InitObjectOnScene();
        InitCharacter(health);
    }

    private void Update()
    {
        Move(direction, moveSpeed);
    }

    private void Move(Vector3 direction, float moveSpeed)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, direction, moveSpeed * Time.deltaTime);
    }

    private void CalcDirection(Vector3 position, Quaternion rotation, float speed)
    {
        direction = position;
    }

    public override float CollideDamage()
    {
        return damage;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("Player"))
        {
            float damage = collision.gameObject.GetComponent<ObjectOnScene>().CollideDamage();
            GetDamage(damage);
        }
    }

    public override void DestroyObjectOnScene()
    {
        EventBus.OnEnemyDestroyed(this.gameObject, 300);
        ParticleSystem vfx = Instantiate(ufoDestroyPrefab, this.transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(this.gameObject);
    }
}
