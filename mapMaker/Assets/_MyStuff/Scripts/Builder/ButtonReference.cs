using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReference : MonoBehaviour {

    public List<Vector2> refBlockList;

    bool touchedDown;

    void OnMouseDown()
    {
        touchedDown = true;
    }

    void OnMouseUp()
    {
        touchedDown = false;
        Vector3 touchpoint = CommonMethods.GetTouchPoint();
        Vector3 snappedTouchPoint = CommonMethods.GetSnappedPoint(touchpoint);

        if((Vector2)snappedTouchPoint != (Vector2)transform.position)
        {
            Debug.Log((Vector2)snappedTouchPoint);
        }
    }
}
