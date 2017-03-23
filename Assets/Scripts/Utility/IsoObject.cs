using UnityEngine;

/// <summary>
/// Defines an isometric object.
/// </summary>
public class IsoObject : MonoBehaviour
{
	/// <summary>
	/// Determines whether or not the object is static, if so depth sorting is skipped.
	/// </summary>
	public bool Static;

	/// <summary>
	/// The offset for the final order in the current sorting layer.
	/// </summary>
	public int Offset;

    private bool _staticDone;

	/// <summary>
	/// Updates the instance, occurs after Update(). Used for depth sorting.
	/// </summary>
	private void LateUpdate()
    {
		// Depth sorting

		// Check if the object is static
		if (Static && _staticDone) return;

        Isometric.DepthSort(gameObject, Offset);

        // Set _staticDone
        _staticDone = true;
    }
}