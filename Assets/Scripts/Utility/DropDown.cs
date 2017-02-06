using UnityEngine;

public class DropDown : MonoBehaviour
{
    [Tooltip("The panel to show/hide.")]
    public GameObject Panel;

    public void TogglePanel()
    {
        // Hides and shows the panel containing the buttons
        Panel.SetActive(!Panel.activeSelf);
    }
}