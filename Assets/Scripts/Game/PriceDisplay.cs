using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to provide objects with an in-game price tag.
/// </summary>
public class PriceDisplay : MonoBehaviour
{
    [Tooltip("The price to display.")]
    public float Price; /// <summary>The price.</summary>
    [Tooltip("The UI element for displaying the price.")]
    public GameObject PriceTag;	/// <summary>The tag.</summary>

    /// <summary>
    /// Called when the pointer enters.
    /// </summary>
    public void MouseEnter()
    {
		// Set text
		PriceTag.GetComponentInChildren<Text>().text = "$" + Price;
        // Show tag
        PriceTag.SetActive(true);
    }

    /// <summary>
    /// Called on pointer exit.
    /// </summary>
    public void MouseExit()
    {
        // Hide tag
        PriceTag.SetActive(false);
    }
}
