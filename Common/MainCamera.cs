using UnityEngine;

public static class MainCamera
{
    public static Vector3 CameraLeftBottomPoint;
    public static Vector3 CameraRightTopPoint;

    public static void Init()
    {
        CameraLeftBottomPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0f));
        CameraRightTopPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }
}
