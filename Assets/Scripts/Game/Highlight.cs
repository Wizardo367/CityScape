// Video used: https://www.youtube.com/watch?v=bpB4BApnKhM

using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color HighlightColour;

    private Color _normalColour;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        // Initilisation
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _normalColour = _spriteRenderer.color;
        HighlightColour.a = 1f;
    }

    private void Update()
    {
        // Check for mouse entry/exit using a ray
        //Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D raycast = Physics2D.Raycast(worldPoint, -Vector2.up);
        //_spriteRenderer.color = raycast.collider != null ? HighlightColour : _normalColour;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = HighlightColour;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _normalColour;
    }
}