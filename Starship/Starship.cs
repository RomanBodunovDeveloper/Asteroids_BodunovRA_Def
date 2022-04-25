using UnityEngine;

public class Starship : Character
{
    private const float maxAccelerate = 1f;

    [SerializeField] private float gasForce         =  0.02f;
    [SerializeField] private float speedLimit       =  0.02f;
    [SerializeField] private float inertionDamping  =  0.997f;
    [SerializeField] private float rotateForce      = -1f;
    [SerializeField] private float health = 1;
    [SerializeField] private ParticleSystem starshipDestroyPrefab;
    [SerializeField] private GameObject[] weaponSlots;

    private GameObject[] weapos;

    private Vector3 moveDist;
    private float   gas;
    private float   accelerate;

    public override void OnEnable()
    {
        base.OnEnable();
        EventBus.PressFireButton1  += ChooseWeapon;
        EventBus.PressFireButton2  += ChooseWeapon;
        EventBus.PressMoveButton   += UpdateAccelerate;
        EventBus.PressRotateButton += Rotate;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventBus.PressFireButton1  -= ChooseWeapon;
        EventBus.PressFireButton2  -= ChooseWeapon;
        EventBus.PressMoveButton   -= UpdateAccelerate;
        EventBus.PressRotateButton -= Rotate;
    }

    private void Awake()
    {
        InitObjectOnScene();
        InitCharacter(health);
        CreateWeapon(weaponSlots);
    }

    private void Update()
    {
        gas  = CalcGas(gas, accelerate, gasForce, maxAccelerate);
        moveDist = CalcMoveDist(moveDist, gas, speedLimit, inertionDamping);
        Move(moveDist);

        EventBus.OnStarshipNavigationChange(this.transform.position, this.transform.rotation, moveDist.magnitude * 100);

        LoopCheckPosition();
    }

    private void UpdateAccelerate(float curAccelerate)
    {
        accelerate = curAccelerate;
    }

    private float CalcGas(float curGas, float accelerate, float gasForce, float maxAccelerate)
    {
        float calcGas = accelerate == maxAccelerate ? Mathf.Clamp(curGas + accelerate * gasForce * Time.deltaTime, 0, maxAccelerate) : 0; 
        return calcGas;
    }

    private Vector3 CalcMoveDist(Vector3 curMove, float curGas, float speedLimit, float inertionDamping)
    {
        Vector3 calcMove;
        calcMove = curMove + this.transform.up * curGas * Time.deltaTime;
        calcMove = Vector3.ClampMagnitude(calcMove, speedLimit);
        calcMove *= inertionDamping;
        return calcMove;
    }

    public override void Rotate(float rotateDirection)
    {
        this.transform.Rotate(Vector3.forward, rotateDirection * rotateForce);
    }

    public override void DestroyObjectOnScene()
    {
        EventBus.OnStarshipDestroed();
        EventBus.OnEndGame();
        ParticleSystem vfx = Instantiate(starshipDestroyPrefab, this.transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(this.gameObject);
    }

    private void ChooseWeapon(int weaponIndex)
    {
        GameObject weapon = weapos[weaponIndex - 1];
        if (weapon != null)
        {
            weapon.GetComponent<Weapon>().PrepareWeapon();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyWeapon"))
        {
            float damage = collision.gameObject.GetComponent<ObjectOnScene>().CollideDamage();
            GetDamage(damage);
        }
    }

    public override void Teleport(Vector3 teleportPosition, GameObject starship)
    {
        if (teleportPosition.x != Vector3.zero.x || teleportPosition.y != Vector3.zero.y)
        {
            starship.transform.position = teleportPosition;
            EventBus.OnStarshipTeleported();
        }
    }

    private void CreateWeapon(GameObject[] weaponSlots)
    {
        weapos = new GameObject[weaponSlots.Length];
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weapos[i] = Instantiate(weaponSlots[i], (this.transform.position + Vector3.up * this.size.y/2), Quaternion.identity);
            weapos[i].transform.SetParent(this.transform);
            EventBus.OnWeaponSlotInit(weapos[i], i);
        }
    }
}
