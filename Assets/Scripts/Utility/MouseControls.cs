// Website used: http://answers.unity3d.com/questions/387800/click-holddrag-move-camera.html

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Defines mouse controls for an application.
/// </summary>
public class MouseControls : MonoBehaviour
{
	/// <summary>
	/// The minimum zoom level.
	/// </summary>
	public float zoomMin = 1;
	/// <summary>
	/// The maximum zoom level.
	/// </summary>
	public float zoomMax = 5;
	/// <summary>
	/// Determines how smooth scrolling should be, higher values equal smoother scrolling.
	/// </summary>
	public float smoothScrollLevel = 4;

	/// <summary>
	/// Determines whether or not to apply camera bounds. Bounds prevent the user from losing focus of the game.
	/// </summary>
	public bool applyCameraBounds;
	/// <summary>
	/// The camera's top left bound.
	/// </summary>
	public Vector2 cameraTopLeftBound;
	/// <summary>
	/// The camera's bottom right bound.
	/// </summary>
	public Vector2 cameraBottomRightBound;

    private Camera _camera;
    private Vector3 origin;
    private float lastMouseScroll;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        // Variable initialisation
        origin = transform.position;
        _camera = Camera.main;
    }

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
    {
        // Zoom
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Approximately(mouseWheel, lastMouseScroll)) return;

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + (!Mathf.Approximately(mouseWheel, 0f) ? -Mathf.Sign(mouseWheel) : 0f) / smoothScrollLevel, zoomMin, zoomMax);
        lastMouseScroll = mouseWheel;
    }

	/// <summary>
	/// Updates the instance, occurs after LateUpdate().
	/// </summary>
	private void LateUpdate()
    {
        // Check if the mouse pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Pan
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
            origin = mousePos;
        else if (Input.GetMouseButton(1))
        {
            // Get difference
            Vector3 difference = transform.position - mousePos;
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