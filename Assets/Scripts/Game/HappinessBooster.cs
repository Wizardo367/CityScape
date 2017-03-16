using UnityEngine;

public class HappinessBooster : MonoBehaviour
{
    [Tooltip("Amount to boost happiness by.")]
    public int Boost;
    [Tooltip("Minimum distance from buildings for the effect to apply.")]
    public float MinDistance = 2f;

    private GameObject _buildings;

    private void Start()
    {
        // Initialisation
        _buildings = GameObject.Find("Game/Tiles/Buildings");
    }

    public void Set(int amount)
    {
        Boost = amount;
    }

    private void Update()
    {
        // Check distance to affected objects
        Building[] childBuildings = _buildings.GetComponentsInChildren<Building>();

        foreach (Building building in childBuildings)
        {
            // Check distance
            float dist = Vector3.Distance(transform.position, building.transform.position);

            // Apply effect
            if (dist <= MinDistance)
                building.Boosters.Add(this);
            else
                building.Boosters.Remove(this);
        }
    }

    private void OnDestroy()
    {
        Building[] childBuildings = _buildings.GetComponentsInChildren<Building>();

        // Removes booster from HashSet to prevent bug
        foreach (Building building in childBuildings)
            building.Boosters.Remove(this);
    }
}