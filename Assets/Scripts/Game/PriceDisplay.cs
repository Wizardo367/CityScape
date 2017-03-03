using UnityEngine;
using UnityEngine.UI;

public class PriceDisplay : MonoBehaviour
{
    [Tooltip("The price to display.")]
    public float Price;
    [Tooltip("The UI element for displaying the price.")]
    public GameObject PriceTag;

    public void MouseEnter()
    {
        // Position tag
        Vector3 position = transform.position;
        position.x -= 50f;
        PriceTag.transform.position = position;
        // Set text
        PriceTag.GetComponentInChildren<Text>().text = "$" + Price;
        // Show tag
        PriceTag.SetActive(true);
    }

    public void MouseExit()
    {
        // Hide tag
        PriceTag.SetActive(false);
    }
}
