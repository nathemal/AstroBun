using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f; 
    public Vector3 offset = new Vector3(0, 2, -10); 
    public bool smoothZoom = true; 
    public float zoomFactor = 0.05f; 
    public float maxZoomOut = 7f; 
    public float minZoomIn = 3f; 
    private Camera cam; //

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (player == null) 
            return; 

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        if (smoothZoom)
        {
            float speed = player.GetComponent<Rigidbody2D>().linearVelocity.magnitude; // Get player speed
            float targetZoom = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(speed * zoomFactor, minZoomIn, maxZoomOut), Time.deltaTime * 2);
            cam.orthographicSize = targetZoom;
        }
    }
}