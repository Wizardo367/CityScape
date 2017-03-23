using UnityEngine;

/// <summary>
/// Defines an object which alters the happiness of surrounding objects.
/// </summary>
public class HappinessBooster : MonoBehaviour
{
    /// <summary>
    /// Define's the amount to boost the happiness levels of eligible buildings.
    /// </summary>
    [Tooltip("Amount to boost happiness by.")]
    public int Boost;
    /// <summary>
    /// Define's the minimum distance a building must be, in order for the effect to apply.
    /// </summary>
    [Tooltip("Minimum distance from buildings for the effect to apply.")]
    public float MinDistance = 2f;

    /// <summary>
    /// The group of buildings to check.
    /// </summary>
    private GameObject _buildings;

    /// <summary>
    /// Used to initialise variables.
    /// </summary>
    private void Start()
    {
        // Initialisation
        _buildings = GameObject.Find("Game/Tiles/Buildings");
    }

    /// <summary>
    /// Called once per frame. Used to check which buildings are eligible for a boost.
    /// </summary>
    private void Update()
    {
        // Check distance to affected objects
        Building[] childBuildings = _buildings.GetComponentsInChildren<Building>();

        foreach (Building building in childBuildings)
        {
            // Check distance
            float dist = Vector2.Distance(transform.position, building.transform.position);

            // Apply effect
            if (dist <= MinDistance)
                building.Boosters.Add(this);
            else
                building.Boosters.Remove(this);
        }
    }

    /// <summary>
    /// Used to clean-up memory whilst the object is being destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Building[] childBuildings = _buildings.GetComponentsInChildren<Building>();

        // Check for null
        if (childBuildings == null) return;

        // Removes booster from HashSet to prevent bug
        foreach (Building building in childBuildings)
            building.Boosters.Remove(this);
    }
}