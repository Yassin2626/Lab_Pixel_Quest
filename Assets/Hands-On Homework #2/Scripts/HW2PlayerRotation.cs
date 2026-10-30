using UnityEngine;

public class HW2PlayerRotation : MonoBehaviour
{
    // Camera Reference
    private string cameraName = "Game_Camera";
    private Camera _camera;

    // Mouse Tracking
    private Vector3 positionMouse;

    private void Start()
    {
        _camera = GameObject.Find(cameraName).GetComponent<Camera>();
    }

    private void Update()
    {
        positionMouse = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 difference = positionMouse - transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);
    }
}
