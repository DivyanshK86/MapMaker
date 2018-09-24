using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public Vector3 Offset;
    public Transform target;
    public float followSpeed = 10f;

    Vector3 lastEditPosition;
    public bool lastEditPosition_Recorded;
    public bool lerpedTo_lastEditPosition;

    public void LateUpdate()
    {
        if (target != null)
        {
            if (ModeManager.gameMode == ModeManager.GameMode.playMode)
            {
                if(!lastEditPosition_Recorded)
                {
                    lastEditPosition = transform.position;
                    lastEditPosition_Recorded = true;
                    lerpedTo_lastEditPosition = false;  
                }

                transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), followSpeed * Time.deltaTime);
            }
            else if (ModeManager.gameMode == ModeManager.GameMode.buttonMode)
            {
                Vector3 newTarget = ModeManager.insatance.buttonReference.refLinesHolder.position;
                transform.position = Vector3.Lerp(transform.position, new Vector3(newTarget.x, newTarget.y, transform.position.z), followSpeed * Time.deltaTime);
            }
            else if(!lerpedTo_lastEditPosition)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(lastEditPosition.x, lastEditPosition.y, transform.position.z), followSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position,lastEditPosition) < 0.1f)
                {
                    lerpedTo_lastEditPosition = true;  
                    lastEditPosition_Recorded = false;
                }
            }
        }

    }
}
