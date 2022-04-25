using UnityEngine;

public class Asteroid : Character
{
    [SerializeField] private float minRotateSpeed = -10f;
    [SerializeField] private float maxRotateSpeed =  10f;
    [SerializeField] private float minMoveSpeed   = -10f;
    [SerializeField] private float maxMoveSpeed   =  10f;
    [SerializeField] private float health         =  5f;
    [SerializeField] private float damage         =  1f;
    [SerializeField] private GameObject[]   asteroidPartPrefab;
    [SerializeField] private ParticleSystem asteroidDestroyPrefab;

    public float rotateSpeed { get; private set; }
    public float moveSpeed { get; private set; }
    public Vector3 direction { get; private set; }

    public virtual void Awake()
    {
        InitObjectOnScene();
        InitCharacter(health);
        InitAsteroid(minRotateSpeed, maxRotateSpeed, minMoveSpeed, maxMoveSpeed, MainCamera.CameraLeftBottomPoint, MainCamera.CameraRightTopPoint);
    }

    private void Update()
    {
        Move(direction * moveSpeed * Time.deltaTime);
        Rotate(rotateSpeed);
        LoopCheckPosition();
    }

    private void InitAsteroid(float minRotateSpeed, float maxRotateSpeed, float minMoveSpeed, float maxMoveSpeed, Vector2 cameraLeftBottomPoint, Vector2 cameraRightTopPoint)
    {
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        moveSpeed   = Random.Range(minMoveSpeed, maxMoveSpeed);
        direction = CalcDirection(this.transform.position, cameraLeftBottomPoint, cameraRightTopPoint);
    }

    private Vector3 CalcDirection(Vector3 position, Vector2 cameraLeftBottomPoint, Vector2 cameraRightTopPoint)
    {
        float minXDir = position.x <= cameraLeftBottomPoint.x ? 0.1f : -1f;
        float maxXDir = position.x >= cameraRightTopPoint.x ? -0.1f : 1f;
        float minYDir = position.y <= cameraLeftBottomPoint.y ? 0.1f : -1f;
        float maxYDir = position.y >= cameraRightTopPoint.y ? -0.1f : 1f;
        return new Vector3 (Random.Range(minXDir, maxXDir), Random.Range(minYDir, maxYDir), 0);
    }

    public override void DestroyObjectOnScene()
    {
        EventBus.OnEnemyDestroyed(this.gameObject, 100);
        CreatePart();
        ParticleSystem vfx = Instantiate(asteroidDestroyPrefab, this.transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("Player"))
        {
            float damage = collision.gameObject.GetComponent<ObjectOnScene>().CollideDamage();
            GetDamage(damage);
        }  
    }

    public override float CollideDamage()
    {
        return damage;
    }

    private void CreatePart()
    {
        for (int i = 0; i < asteroidPartPrefab.Length; i++)
        {
            Instantiate(asteroidPartPrefab[i], this.transform.position, Quaternion.identity);
        }
    }

    public ParticleSystem GetAsteroidDestroyPrefab()
    {
        return asteroidDestroyPrefab;
    }
}
