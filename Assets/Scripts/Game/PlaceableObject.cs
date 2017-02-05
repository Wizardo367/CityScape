using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [Range(0.0f, 1.0f), Tooltip("The alpha value of the object before it's placed.")] public float HoverAlpha = 0.5f;
    public GameObject target;

    private GameObject _go;
    private SpriteRenderer[] _spriteRenderers;
    private Quaternion currentRotation;
    private Color _defaultColour;
    private Map2D _map;


    public void Init()
    {
        // Initialise object
        _go = Instantiate(target);
        _defaultColour = Color.white;
        // Set alpha
        _spriteRenderers = _go.GetComponentsInChildren<SpriteRenderer>();
        ToggleAlpha();
    }

    private void Start()
    {
        _map = GameObject.Find("Game").GetComponent<Map2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the object is null
        if (_go == null) return;

        // Follow mouse/current tile till placed
        Vector3 tilePos = _map.GetCurrentTile().transform.position;
        _go.transform.position = tilePos;

        // Check for keyboard input
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Rotate
            currentRotation.y = currentRotation.y >= 0 ? -180f : 0f;
            _go.transform.rotation = currentRotation;
        }

        // Check for clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Object placed

            // Set alpha
            ToggleAlpha();

            // Activate marker script if found
            MonoBehaviour markerScript = _go.GetComponent<Marker>();
            if (markerScript != null)
            {
                // Enable script
                markerScript.enabled = true;
                // Rotation
                ((Marker)markerScript).SetFinalRotation(currentRotation);
            }
            else
            {
                // Rotation
                _go.transform.rotation = currentRotation;
            }

            // Set go to null
            _go = null;
        }
    }

    private void ToggleAlpha()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            Color currentColor = spriteRenderer.color;
            Color newColour = _defaultColour;
            newColour.a = currentColor.a == HoverAlpha ? 1f : HoverAlpha;
            spriteRenderer.color = newColour;
        }
    }
}
