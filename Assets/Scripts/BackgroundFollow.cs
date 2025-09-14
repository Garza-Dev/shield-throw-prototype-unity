using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxFactor = 0.5f; // 0 = static, 1 = moves with camera

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = startPos + (cameraTransform.position * parallaxFactor);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
