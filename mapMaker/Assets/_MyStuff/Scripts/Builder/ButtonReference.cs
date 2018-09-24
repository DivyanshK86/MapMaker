using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReference : MonoBehaviour {

    public List<Vector2> refBlockList;
    public Transform refLinesHolder;
    public GameObject _LinePrefab;
    public GameObject _BlockToActivatePrefab;

    public bool drawingEnabled;
    GameObject tempLine;

    bool touchedDown;

    void OnMouseDown()
    {
        if(drawingEnabled)
        {
            touchedDown = true;
            tempLine = Instantiate(_LinePrefab,transform.position,Quaternion.identity) as GameObject;
        }
    }

    void OnMouseUp()
    {
        if(drawingEnabled)
        {
            touchedDown = false;
            Vector3 touchpoint = CommonMethods.GetTouchPoint();
            Vector3 snappedTouchPoint = CommonMethods.GetSnappedPoint(touchpoint);

            if((Vector2)snappedTouchPoint != (Vector2)transform.position)
            {
                Debug.Log((Vector2)snappedTouchPoint);
                refBlockList.Add((Vector2)snappedTouchPoint);
                UpdateReferenceLines();
            }
        }
    }

    void Update()
    {
        if(drawingEnabled)
        {
            if(touchedDown)
            {
                Vector3 touchpoint = CommonMethods.GetTouchPoint();
                Vector3 snappedTouchPoint = CommonMethods.GetSnappedPoint(touchpoint);
                Vector2 v2 = (Vector2)snappedTouchPoint;

                ManipulateLineAngle(v2, tempLine);
            }
            else
            {
                Destroy(tempLine);
            }
        }
    }

    public void UpdateReferenceLines()
    {
        foreach(Transform child in refLinesHolder)
        {
            Destroy(child.gameObject);
        }

        foreach(Vector2 v2 in refBlockList)
        {
            GameObject lineGo = Instantiate(_LinePrefab,transform.position,Quaternion.identity) as GameObject;
            lineGo.transform.SetParent(refLinesHolder);

            ManipulateLineAngle(v2, lineGo);
        }
    }

    void ManipulateLineAngle(Vector2 v2, GameObject lineObject)
    {
        float dist = Vector2.Distance((Vector2)transform.position, v2);
        lineObject.transform.localScale = new Vector3(dist * 2, 0.1f, 1);
        Vector2 dir = v2 - (Vector2)transform.position;
        float angle = SignedAngle(transform.right, dir);
        lineObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    float SignedAngle( Vector3 from, Vector3 to)
    {
        float angle = Vector3.Angle( from, to );
        float sign = Mathf.Sign( Vector3.Dot( Vector3.up, to - from) );
        float finalAngle = (angle * sign);
        return finalAngle;
    }
}
