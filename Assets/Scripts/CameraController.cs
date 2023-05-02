using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseDelta;

    public float moveSpeed;
    public float zoomSpeed;

    public float minZoomDistance;
    public float maxZoomDistance;

    // Update is called once per frame
    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        var mouseInput = Input.mousePosition;

        if (mouseInput.x >= Screen.width - mouseDelta)
            ApplyVector(Vector3.right);
        else if (mouseInput.x <= mouseDelta) ApplyVector(Vector3.left);

        if (mouseInput.y >= Screen.height - mouseDelta) ApplyVector(Vector3.up);
        else if (mouseInput.y <= mouseDelta) ApplyVector(Vector3.down);
        /*
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * moveSpeed * Time.deltaTime,
            Input.GetAxisRaw("Mouse Y") * moveSpeed * Time.deltaTime, 0f);
    */

        void ApplyVector(Vector3 vector) => transform.position += vector * (moveSpeed * Time.deltaTime);
    }

    private void Zoom()
    {
    }
}