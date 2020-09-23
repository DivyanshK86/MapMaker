using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float speed;
    public float zoomAmount = 0.5f;

    float orthSize;
    float framesToSkip = 4;
    float skippedFrames;

    void Start()
    {
        orthSize = Camera.main.orthographicSize;
        skippedFrames = framesToSkip;
    }

    void LateUpdate()
    {
        if(ModeManager.gameMode == ModeManager.GameMode.viewMode)
        {
            if (Input.GetMouseButton(0))
            {
                if (skippedFrames > 0)
                    skippedFrames--;
                else
                    transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed * Camera.main.orthographicSize * Time.deltaTime, Input.GetAxis("Mouse Y") * speed * Camera.main.orthographicSize * Time.deltaTime, 0);
            }
            else
                skippedFrames = framesToSkip;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
            {
                orthSize -= zoomAmount;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
            {
                orthSize += zoomAmount;
            }
        }


        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthSize, 10 * Time.deltaTime);
    }

    public void _ZoomInBtn()
    {
        orthSize -= zoomAmount;
    }

    public void _ZoomOutBtn()
    {
        orthSize += zoomAmount;
    }
}