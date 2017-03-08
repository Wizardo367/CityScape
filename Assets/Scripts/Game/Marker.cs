using System;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public TileType Type;
    public GameObject Target;
    [Tooltip("Delay till the target is shown, in seconds.")]
    public float Delay = 5;

    private float _timeRemaining;

    private Quaternion _finalRotation;

    private void Start()
    {
        _timeRemaining = Delay;
    }

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

    public void SetFinalRotation(Quaternion rotation)
    {
        _finalRotation = rotation;
    }
}