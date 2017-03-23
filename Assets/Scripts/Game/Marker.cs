using System;
using UnityEngine;

/// <summary>
/// Used to mark the spawn location of an object.
/// </summary>
public class Marker : MonoBehaviour
{
    /// <summary>
    /// The type.
    /// </summary>
    public TileType Type;
    /// <summary>
    /// The GameObject to be spawned.
    /// </summary>
    public GameObject Target;
    [Tooltip("Delay till the target is shown, in seconds.")]
    public float Delay = 5; /// <summary> The delay before the target is spawned. </summary>

    private float _timeRemaining;

    private Quaternion _finalRotation;

    /// <summary>
    /// Initialises this instance.
    /// </summary>
    private void Start()
    {
        _timeRemaining = Delay;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void Update()
    {
        // Countdown then display target
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0)
        {
            // Get map
            Map2D map = GameObject.Find("Game").GetComponent<Map2D>();

            // Get building type (Assumes Type is set correctly)
            TileType buildingType;

            switch (Type)
            {
                case TileType.CommercialMarker:
                    buildingType = TileType.Commercial;
                    break;
                case TileType.OfficeMarker:
                    buildingType = TileType.Office;
                    break;
                case TileType.ResidentialMarker:
                    buildingType = TileType.Residential;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Spawn building
            map.SpawnBuilding(buildingType, 1, gameObject.transform.position, _finalRotation);

            // Remove marker
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets the final rotation.
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    public void SetFinalRotation(Quaternion rotation)
    {
        _finalRotation = rotation;
    }
}