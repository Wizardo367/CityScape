using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    private Vector3 _origin;
    private bool onObject;

    private void Start()
    {
        // Variable initialisation
        _origin = transform.position;
        // Add collider
        if (!gameObject.GetComponent<Collider2D>())
            gameObject.AddComponent<BoxCollider2D>();
    }

    private void OnMouseEnter()
    {
        onObject = true;
    }

    private void OnMouseExit()
    {
        onObject = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO Code copied in MouseControls, put in shared method
        // Check if the mouse pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            // Check if mouse is over object
            if (onObject)
            {
                // Set position
                gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            }
        }
    }
}