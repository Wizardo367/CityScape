using UnityEngine;

/// <summary>
/// Used to toggle the active state of a panel.
/// </summary>
public class DropDown : MonoBehaviour
{
    [Tooltip("The panel to show/hide.")]
    public GameObject Panel; /// <summary>The panel to toggle.</summary>

	/// <summary>
	/// Toggles the active state of the panel.
	/// </summary>
	public void TogglePanel()
    {
        // Hides and shows the panel containing the buttons
        Panel.SetActive(!Panel.activeSelf);
    }
}