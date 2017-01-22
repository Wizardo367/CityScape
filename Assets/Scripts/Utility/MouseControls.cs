// Website used: http://answers.unity3d.com/questions/387800/click-holddrag-move-camera.html

using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControls : MonoBehaviour
{
    public float zoomMin = 1;
    public float zoomMax = 5;
    public float smoothScrollLevel = 4;

    public bool applyCameraBounds;
    public Vector2 cameraTopLeftBound;
    public Vector2 cameraBottomRightBound;

    private Vector3 origin;

    private float lastMouseScroll;

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        // Zoom
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Approximately(mouseWheel, lastMouseScroll)) return;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + (Mathf.Approximately(mouseWheel, 0f) ? -Mathf.Sign(mouseWheel) : 0f) / smoothScrollLevel,zoomMin, zoomMax);
        lastMouseScroll = mouseWheel;
    }

    void LateUpdate()
    {
        // Check if the mouse pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Pan
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = transform.position - mousePos;

        if (Input.GetMouseButtonDown(0))
            origin = mousePos;
        else if (Input.GetMouseButton(0))
        {
            // Calculate new position
            Vector3 newPosition = new Vector3(origin.x + difference.x, origin.y + difference.y, -10);

            // Check new position
            if (applyCameraBounds)
            {
                if (newPosition.x > cameraTopLeftBound.x)
                    newPosition.x = cameraTopLeftBound.x;
                else if (newPosition.x < cameraBottomRightBound.x)
                    newPosition.x = cameraBottomRightBound.x;

                if (newPosition.y < cameraTopLeftBound.y)
                    newPosition.y = cameraTopLeftBound.y;
                else if (newPosition.y > cameraBottomRightBound.y)
                    newPosition.y = cameraBottomRightBound.y;
            }

            // Set position
            transform.position = newPosition;
        }
    }
}
