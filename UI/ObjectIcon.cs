using UnityEngine;

public class ObjectIcon : MonoBehaviour
{
    [SerializeField] private Sprite objectIcon;

    public Sprite GetObjectIcon()
    {
        return objectIcon;
    }
}
