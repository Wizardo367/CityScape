using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Defines an object as draggable by the mouse.
/// </summary>
public class Draggable : MonoBehaviour
{
    private bool onObject;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        // Add collider
        if (!gameObject.GetComponent<Collider2D>())
            gameObject.AddComponent<BoxCollider2D>();
    }

	/// <summary>
	/// Called when [mouse enter].
	/// </summary>
	private void OnMouseEnter()
    {
        onObject = true;
    }

	/// <summary>
	/// Called when [mouse exit].
	/// </summary>
	private void OnMouseExit()
    {
        onObject = false;
    }

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
    {
        // Check if the mouse pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Check for a click and if the pointer is over the object
        if (Input.GetMouseButton(0) && onObject)
        {
            // Get mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Set position
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }
}