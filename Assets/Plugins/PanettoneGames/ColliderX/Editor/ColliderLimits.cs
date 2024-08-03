using UnityEngine;


/// <summary>
/// General Settings that works for most games.
/// </summary>
//[CreateAssetMenu(menuName = "ColliderX")] //Uncomment to create one object to control the global settings.
//there's already one scriptable object asset provided and you don't actually need to create another one, just find it and change its variables
public class ColliderLimits : ScriptableObject
{
    public int VertLimit;
    [Range(0.1f, 1f)]
    public float BlockinessThreshold;
}
