using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float speed;

    void LateUpdate()
    {
        if(Input.GetMouseButton(2))
        {
            transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed * Camera.main.orthographicSize  * Time.deltaTime, Input.GetAxis("Mouse Y") * speed * Camera.main.orthographicSize * Time.deltaTime, 0);
        }
    }
}