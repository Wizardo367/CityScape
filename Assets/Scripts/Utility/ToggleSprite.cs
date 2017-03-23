using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows an Image component to toggle between two images.
/// </summary>
public class ToggleSprite : MonoBehaviour
{
	/// <summary>
	/// The primary sprite.
	/// </summary>
	public Sprite PrimarySprite;
	/// <summary>
	/// The secondary sprite.
	/// </summary>
	public Sprite SecondarySprite;

	private Sprite _currentSprite;
    private Image _image;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        _currentSprite = PrimarySprite;
        _image = gameObject.GetComponent<Image>();
    }

	/// <summary>
	/// Toggles the current image being displayed.
	/// </summary>
	public void Toggle()
    {
        // Toggle button graphic
        _currentSprite = _currentSprite == PrimarySprite ? SecondarySprite : PrimarySprite;
        SetSprite();
    }

	/// <summary>
	/// Sets which image should be displayed.
	/// </summary>
	/// <param name="primaryState">if set to <c>true</c> [primary state].</param>
	public void Set(bool primaryState)
    {
        _currentSprite = primaryState ? PrimarySprite : SecondarySprite;
        SetSprite();
    }

	/// <summary>
	/// Sets the current image.
	/// </summary>
	private void SetSprite()
    {
        _image.sprite = _currentSprite;
    }
}