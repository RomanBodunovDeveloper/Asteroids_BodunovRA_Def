using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage { get; private set; }
    public float ReloadTime { get; private set; }
    public float MaxAmmo { get; private set; }

    public GameObject ParentObject { get; private set; }

    private float curAmmo;
    private bool  reolading;
    private float curReloadTime;
    private float reloadTimeStep = 0.1f;

    public void InitWeapon(float damage, float reloadTime, float maxAmmo, GameObject parentObject)
    {
        Damage       = damage;
        ReloadTime   = reloadTime;
        MaxAmmo      = curAmmo = maxAmmo;
        ParentObject = parentObject;
    }

    private void Start()
    {
        EventBus.OnWeaponReloadTimeUpdate(ParentObject, curReloadTime);
        EventBus.OnWeaponAmmoUpdate(ParentObject, curAmmo);
    }

    public void PrepareWeapon()
    {
        if (CanAttack())
        {
            AttackWeapon();
            if (!reolading)
            {
                StartCoroutine(ReloadWeapon(ReloadTime));
            }
        }
    }

    IEnumerator ReloadWeapon(float reloadTime)
    {
        reolading = true;
        curReloadTime = reloadTime;

        while (curReloadTime > 0)
        {
            yield return new WaitForSeconds(reloadTimeStep);
            DecreaseReloadTime(reloadTimeStep);
        }
        
         IncreaseAmmo(1);
         if (curAmmo < MaxAmmo)
         {
            StartCoroutine(ReloadWeapon(ReloadTime));
         }
         else
         {
             reolading = false;
         }
    }

    public virtual void AttackWeapon()
    {
        DecreaseAmmo(1);
    }

    private bool CanAttack()
    {
       return curAmmo > 0;
    }

    private void DecreaseReloadTime(float val)
    {
        if (curReloadTime > 0)
        {
            curReloadTime -= val;
            if (curReloadTime < 0)
            {
                curReloadTime = 0;
            }
            EventBus.OnWeaponReloadTimeUpdate(ParentObject, curReloadTime);
        }     
    }

    private void DecreaseAmmo(float val)
    {
        if (curAmmo > 0)
        {
            curAmmo -= val;
            EventBus.OnWeaponAmmoUpdate(ParentObject, curAmmo);
        }
    }

    private void IncreaseAmmo(float val)
    {
        if (curAmmo < MaxAmmo)
        {
            curAmmo += val;
            EventBus.OnWeaponAmmoUpdate(ParentObject, curAmmo);
        }   
    }
}
