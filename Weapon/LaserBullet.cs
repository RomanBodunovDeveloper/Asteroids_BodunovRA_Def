using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LaserBullet: ObjectOnScene
{
    [SerializeField] private ParticleSystem laserPrepareVFX;
    [SerializeField] private float laserMaxLenght = 10f;
    [SerializeField] private float laserMinWidth = 0f;
    [SerializeField] private float laserMaxWidth = 0.05f;
    [SerializeField] private float laserPrepareTime = 2;
    [SerializeField] private float laserAttackTime = 3;
    [SerializeField] private float laserGrowSpeed = 0.1f;
    private LineRenderer   laserLine;
    private EdgeCollider2D laserCollider;
    private bool  laserPrepared;
    private float laserCurLenght;
    private float damage;
    private Transform laserGunTransform;


    public override void OnEnable()
    {
        base.OnEnable();
        EventBus.StarshipTeleported += DestroyObjectOnScene;
        EventBus.StarshipDestroed += DeleteObjectOnScene;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        EventBus.StarshipTeleported -= DestroyObjectOnScene;
        EventBus.StarshipDestroed -= DeleteObjectOnScene;
    }

    public void InitLaserBullet(Transform laserGuntransform, float damage)
    {
        this.damage = damage;
        this.laserGunTransform = laserGuntransform;
    }

    private void Update()
    {
        this.transform.position = laserGunTransform.position;

        if (laserPrepared)
        {
            if (laserCurLenght < laserMaxLenght)
            {
                laserCurLenght += laserGrowSpeed;
            }
        }
        SetLaserColliderPoints(laserCollider, laserCurLenght);
        SetLaserLength(laserLine, laserCurLenght);
    }

    private void Awake()
    {
        laserLine     = this.gameObject.GetComponent<LineRenderer>();
        laserCollider = this.gameObject.GetComponent<EdgeCollider2D>();
        SetLaserWidth(laserLine, laserMinWidth);
        var mainLaserPrepareVFX = laserPrepareVFX.main;
        mainLaserPrepareVFX.startLifetime = laserPrepareTime + laserAttackTime;

        ParticleSystem vfx =  Instantiate(laserPrepareVFX, this.transform.position, Quaternion.identity) as ParticleSystem;
        vfx.transform.SetParent(this.transform);

        StartCoroutine(LaserPreparing(laserPrepareTime));
    }

    private void SetLaserWidth(LineRenderer laserLine, float width)
    {
        laserLine.startWidth = width;
        laserLine.endWidth = width;
    }

    public void SetLaserLength(LineRenderer laserLine, float length)
    {
        Vector3 startPosition = laserGunTransform.position;
        Vector3 endPosition = startPosition + laserGunTransform.up * length;

        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition((laserLine.positionCount - 1), endPosition);
    }

    public void SetLaserColliderPoints(EdgeCollider2D laserCollider, float length)
    {
        Vector3 testStartPosition = laserGunTransform.localPosition;
        Vector3 testEndPosition = testStartPosition + laserGunTransform.up * length;

        testEndPosition = this.transform.InverseTransformDirection(testEndPosition);

        Vector2[] pointsCol = new Vector2[] { new Vector2(testStartPosition.x, testStartPosition.y), new Vector2(testEndPosition.x, testEndPosition.y) };
        laserCollider.points = pointsCol;
    }

    IEnumerator LaserPreparing(float laserPrepareTime)
    {
        yield return new WaitForSeconds(laserPrepareTime);
        laserPrepared = true;
        SetLaserWidth(laserLine, laserMaxWidth);
        StartCoroutine(LaserAttacking(laserAttackTime)); 
    }

    IEnumerator LaserAttacking(float laserAttackTime)
    {
        yield return new WaitForSeconds(laserAttackTime);
        DestroyObjectOnScene();
    }

    public override float CollideDamage()
    {
        return damage;
    }

    public override void DestroyObjectOnScene()
    {
        Destroy(this.gameObject);
    }
}
