using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private int weaponSlotNum;
    private Image weaponIcon;
    private Text  reloadTimeText;
    private Text  curAmmoText;
    private GameObject weapon;
    private void Awake()
    {
        weaponIcon     = this.transform.Find("WeaponIcon").gameObject.GetComponent<Image>();
        reloadTimeText = this.transform.Find("ReloadTime").gameObject.GetComponent<Text>();
        curAmmoText    = this.transform.Find("CurAmmo").gameObject.GetComponent<Text>();
    }
    private void OnEnable()
    {
        EventBus.WeaponSlotInit         += Init;
        EventBus.WeaponAmmoUpdate       += AmmoUpdate;
        EventBus.WeaponReloadTimeUpdate += ReloadTimeUpdate;
    }

    private void OnDisable()
    {
        EventBus.WeaponSlotInit         -= Init;
        EventBus.WeaponAmmoUpdate       -= AmmoUpdate;
        EventBus.WeaponReloadTimeUpdate -= ReloadTimeUpdate;
    }

    private void Init(GameObject weapon, int slotNum)
    {
        if (slotNum != weaponSlotNum - 1)
        {
            return;
        }
        this.weapon = weapon;
        weaponIcon.sprite = weapon.GetComponent<ObjectIcon>().GetObjectIcon();
    }

    private void ReloadTimeUpdate(GameObject weapon, float curReloadTime)
    {
        if (weapon != this.weapon)
        {
            return;
        }
        reloadTimeText.text = "Reload: " + curReloadTime.ToString("F1");
    }

    private void AmmoUpdate(GameObject weapon, float curAmmo)
    {
        if (weapon != this.weapon)
        {
            return;
        }
        curAmmoText.text = "Ammo: " + curAmmo;
    }
}
