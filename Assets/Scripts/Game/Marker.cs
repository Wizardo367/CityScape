using UnityEngine;

public class Marker : MonoBehaviour
{
    public TileType Type;
    public GameObject Target;
    [Tooltip("Delay till the target is shown, in seconds.")]
    public float Delay = 5;

    private Time _startTime;
    private float _timeRemaining;

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
            string typeString = Type.ToString();
            TileType buildingType = (TileType)System.Enum.Parse(typeof(TileType), typeString.Substring(0, typeString.Length - 6));

            // Spawn building
            map.SpawnBuilding(buildingType, 1, 1, gameObject.transform.position);

            // Remove marker
            Destroy(gameObject);
        }
    }
}
