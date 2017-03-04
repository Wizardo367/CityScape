using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{
    public Sprite PrimarySprite;
    public Sprite SecondarySprite;
    private Sprite currentSprite;

    private Image _image;

    private void Start()
    {
        currentSprite = PrimarySprite;
        _image = gameObject.GetComponent<Image>();
    }

    public void Toggle()
    {
        // Toggle button graphic
        currentSprite = currentSprite == PrimarySprite ? SecondarySprite : PrimarySprite;
        SetSprite();
    }

    public void Set(bool primaryState)
    {
        currentSprite = primaryState ? PrimarySprite : SecondarySprite;
        SetSprite();
    }

    private void SetSprite()
    {
        _image.sprite = currentSprite;
    }
}