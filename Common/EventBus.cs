using System;
using UnityEngine;

public class EventBus
{
    public delegate void IntAction(int intParam);
    public delegate void FloatAction(float floatParam);
    public delegate void NavigationAction(Vector3 vector3Param, Quaternion quaternionParam, float floatParam);
    public delegate void ObjectFloatAction(GameObject objectParam, float floatParam);
    public delegate void ObjectIntAction(GameObject objectParam, int intParam);
    public delegate void TypeInt(Type typeParam, int intParam);

    public static event IntAction   PressFireButton1;
    public static event IntAction   PressFireButton2;
    public static event FloatAction PressMoveButton;
    public static event FloatAction PressRotateButton;
    public static event Action      PressSettingsButton;

    public static event Action            StarshipDestroed;
    public static event Action            StarshipTeleported;
    public static event ObjectIntAction   WeaponSlotInit;
    public static event ObjectFloatAction WeaponReloadTimeUpdate;
    public static event ObjectFloatAction WeaponAmmoUpdate;
    public static event NavigationAction  StarshipNavigationChange;

    public static event Action    LaunchGame;
    public static event Action    StartGame;
    public static event Action    EndGame;
    public static event IntAction UpdateScore;
    public static event IntAction SendEndGameInfo;

    public static event ObjectIntAction EnemyDestroyed;

    public static event Action ResetSubscribes;

    public static void OnLaunchGame()
    {
        LaunchGame?.Invoke();
    }
    public static void OnStartGame()
    {
        StartGame?.Invoke();
    }
    public static void OnEndGame()
    {
        EndGame?.Invoke();
    }
    public static void OnWeaponAmmoUpdate(GameObject objectParam, float floatParam)
    {
        WeaponAmmoUpdate?.Invoke(objectParam, floatParam);
    }
    public static void OnWeaponReloadTimeUpdate(GameObject objectParam, float floatParam)
    {
        WeaponReloadTimeUpdate?.Invoke(objectParam, floatParam);
    }
    public static void OnWeaponSlotInit(GameObject objectParam, int intParam)
    {
        WeaponSlotInit?.Invoke(objectParam, intParam);
    }
    public static void OnStarshipNavigationChange(Vector3 position, Quaternion rotation, float speed)
    {
        StarshipNavigationChange?.Invoke(position, rotation, speed);
    }
    public static void OnStarshipTeleported()
    {
        StarshipTeleported?.Invoke();
    }
    public static void OnEnemyDestroyed(GameObject typeParamm, int intParam)
    {
        EnemyDestroyed?.Invoke(typeParamm, intParam);
    }
    public static void OnStarshipDestroed()
    {
        StarshipDestroed?.Invoke();
    }
    public static void OnPressFireButton1(int weaponIndex)
    {
        PressFireButton1?.Invoke(weaponIndex);
    }
    public static void OnPressFireButton2(int weaponIndex)
    {
        PressFireButton2?.Invoke(weaponIndex);
    }
    public static void OnPressMoveButton(float floatParam)
    {
        PressMoveButton?.Invoke(floatParam);
    }
    public static void OnPressRotateButton(float floatParam)
    {
        PressRotateButton?.Invoke(floatParam);
    }
    public static void OnPressSettingsButton()
    {
        PressSettingsButton?.Invoke();
    }
    public static void OnUpdateScore(int score)
    {
        UpdateScore?.Invoke(score);
    }
    public static void OnResetSubscribes()
    {
        ResetSubscribes?.Invoke();
    }
    public static void OnSendEndGameInfo(int score)
    {
        SendEndGameInfo?.Invoke(score);
    }
}
