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
            string typeString = Type.ToString();
            TileType buildingType = (TileType)System.Enum.Parse(typeof(TileType), typeString.Substring(0, typeString.Length - 6));

            // Spawn building
            int variant = Random.Range(1, 1); // Last number is exclusive
            map.SpawnBuilding(buildingType, variant, 1, gameObject.transform.position, _finalRotation);

            // Remove marker
            Destroy(gameObject);
        }
    }

    public void SetFinalRotation(Quaternion rotation)
    {
        _finalRotation = rotation;
    }
}