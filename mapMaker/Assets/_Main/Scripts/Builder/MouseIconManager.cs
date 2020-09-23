using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIconManager : MonoBehaviour {

    public Transform mouseObject;
    public List<Sprite> spriteList;

    SpriteRenderer mouseSpriteRenderer;
    int selectedBlock = 0;

    void Start()
    {
        mouseSpriteRenderer = mouseObject.GetComponent<SpriteRenderer>();
        mouseSpriteRenderer.sprite = spriteList[selectedBlock];
    }

	void Update () {

        if(Input.GetMouseButtonDown(1))
        {
            selectedBlock++;
            if (selectedBlock >= spriteList.Count)
                selectedBlock = 0;

            mouseSpriteRenderer.sprite = spriteList[selectedBlock];
        }
		
        mouseObject.position = GetSnappedTouchPoint(GetTouchPoint());
	}

    Vector3 GetTouchPoint()
    {
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPoint.z = 0;
        return touchPoint;
    }

    Vector3 GetSnappedTouchPoint(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }
}
