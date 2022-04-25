using UnityEngine;

public class ObjectOnScene : MonoBehaviour
{
    private Vector2 cameraBorderCrossing;
    private Vector3 teleportPosition;
    public Vector2 size { get; private set; }

    public virtual void Move(Vector3 moveVector)
    {
        this.transform.Translate(moveVector, Space.World);
    }

    public virtual void Rotate(float rotateSpeed)
    {
        this.transform.Rotate(Vector3.forward, rotateSpeed);
    }

    public void InitObjectOnScene()
    {
        size = CommonFunctions.CalcSize(this.gameObject);
    }

    public void LoopCheckPosition()
    {
        cameraBorderCrossing = CheckPosition(MainCamera.CameraLeftBottomPoint, MainCamera.CameraRightTopPoint, this.gameObject.transform.position, size);
        teleportPosition = CalcTeleportPosition(cameraBorderCrossing, this.gameObject.transform.position);
        Teleport(teleportPosition, this.gameObject);
    }

    public Vector2 CheckPosition(Vector2 cameraLeftBottomPoint, Vector2 cameraRightTopPoint, Vector3 starshipPosition, Vector2 size)
    {
        Vector2 cameraBorderCrossing = new Vector2(0, 0);
        float borderSpace = size.x > size.y ? 0.75f * size.x : 0.75f * size.y;

        if (((starshipPosition.x - borderSpace) >= cameraRightTopPoint.x) ||
                  ((starshipPosition.x + borderSpace) <= cameraLeftBottomPoint.x))
        {
            cameraBorderCrossing += new Vector2(1, 0);
        }

        if (((starshipPosition.y - borderSpace) >= cameraRightTopPoint.y) ||
             ((starshipPosition.y + borderSpace) <= cameraLeftBottomPoint.y))
        {
            cameraBorderCrossing += new Vector2(0, 1);
        }

        return cameraBorderCrossing;
    }

    public Vector3 CalcTeleportPosition(Vector2 cameraBorderCrossing, Vector3 position)
    {
        Vector3 teleportPosition = new Vector3(0, 0, 0);

        if (cameraBorderCrossing.x > 0)
        {
            teleportPosition = new Vector3(position.x * (-1), position.y, position.z);
        }

        if (cameraBorderCrossing.y > 0)
        {
            teleportPosition = new Vector3(position.x, position.y * (-1), position.z);
        }

        return teleportPosition;
    }

    public virtual void Teleport(Vector3 teleportPosition, GameObject starship)
    {
        if (teleportPosition.x != Vector3.zero.x || teleportPosition.y != Vector3.zero.y)
        {
            starship.transform.position = teleportPosition;
        }
    }

    public virtual float CollideDamage()
    {
        return 0f;
    }

    public virtual void DestroyObjectOnScene()
    {
    }

    public virtual void OnEnable()
    {
        EventBus.StartGame += DeleteObjectOnScene;
    }

    public virtual void OnDisable()
    {
        EventBus.StartGame -= DeleteObjectOnScene;
    }

    public void DeleteObjectOnScene()
    {
        Destroy(this.gameObject);
    }
}
