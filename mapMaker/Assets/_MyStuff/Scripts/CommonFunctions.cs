using UnityEngine;

public class CommonMethods {

    public static Vector3 GetTouchPoint()
    {
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPoint.z = 0;
        return touchPoint;
    }

    public static Vector3 GetSnappedPoint(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }
}
