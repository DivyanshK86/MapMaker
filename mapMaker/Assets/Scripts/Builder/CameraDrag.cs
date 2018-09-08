using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float speed;
    public float zoomAmount = 0.5f;

    float orthSize;

    void Start()
    {
        orthSize = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        if(Input.GetMouseButton(2))
        {
            transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed * Camera.main.orthographicSize  * Time.deltaTime, Input.GetAxis("Mouse Y") * speed * Camera.main.orthographicSize * Time.deltaTime, 0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
        {
            orthSize -= zoomAmount;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            orthSize += zoomAmount;
        }

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthSize, 10 * Time.deltaTime);
    }
}