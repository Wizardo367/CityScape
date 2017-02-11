using UnityEngine;

public class PlaceableObjectSpawner : MonoBehaviour
{
    [Range(0.0f, 1.0f), Tooltip("The alpha value of the object before it's placed.")] public float HoverAlpha = 0.5f;
    public GameObject Target;

    private GameObject _go;
    private SpriteRenderer[] _spriteRenderers;
    private Quaternion _currentRotation;
    private Color _defaultColour;
    private Map2D _map;


    public void Init()
    {
        // Initialise object
        _go = Instantiate(Target);
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
        Tile currentTile = _map.GetCurrentTile();
        Vector3 tilePos = currentTile.transform.position;
        _go.transform.position = tilePos;

        // Check for keyboard input
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Rotate
            Rotatable2D rotatable2D = _go.GetComponent<Rotatable2D>();
            rotatable2D.Rotate();

            if (rotatable2D != null)
            {
                _currentRotation = rotatable2D.gameObject.transform.rotation;
                _go.transform.rotation = _currentRotation;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cancel placement
            Destroy(_go);
        }

        // Check for clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Object placed

            // Check if the tile is buildable
            if (!currentTile.Buildable) return;

            // Set alpha
            ToggleAlpha();

            // Activate marker script if found
            MonoBehaviour markerScript = _go.GetComponent<Marker>();
            if (markerScript != null)
            {
                // Enable script
                markerScript.enabled = true;
                // Rotation
                ((Marker)markerScript).SetFinalRotation(_currentRotation);
            }
            else
            {
                // Rotation
                _go.transform.rotation = _currentRotation;
            }

            // Disable builadable property of the tile
            currentTile.Buildable = false;

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