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
        switch (Input.mousePosition.x)
        {
            
        }
        if (Input.mousePosition.x >= Screen.width - mouseDelta)
        {
            transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
        }
        
        /*
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * moveSpeed * Time.deltaTime,
            Input.GetAxisRaw("Mouse Y") * moveSpeed * Time.deltaTime, 0f);
    */
    }

    private void Zoom()
    {
        
    }
}