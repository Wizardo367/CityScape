using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{
    public Sprite PrimarySprite;
    public Sprite SecondarySprite;
    private Sprite currentSprite;

    private void Start()
    {
        currentSprite = PrimarySprite;
    }

    public void Toggle()
    {
        // Toggle button graphic
        currentSprite = currentSprite == PrimarySprite ? SecondarySprite : PrimarySprite;
        gameObject.GetComponent<Image>().sprite = currentSprite;
    }
}