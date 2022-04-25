using UnityEngine;

public static class CommonFunctions
{
    public static Vector2 CalcSize(GameObject onject)
    {
        Vector2 calcSize;
        calcSize.x = onject.transform.localScale.x / onject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        calcSize.y = onject.transform.localScale.y / onject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        return calcSize;
    }
}
