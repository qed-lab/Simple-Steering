using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Path Following Steering behavior for agents.
/// </summary>
public class DynamicPathFollowing : DynamicSeek
{
    [Header("Path to Follow")]
    [Tooltip("A path object in the scene.")]
    public Path Path;

    [Header("Steering Smoothing")]
    [Tooltip("A higher indicates more smoothing.")]
    [Range(1, 4000)]
    public float PathOffset = 0f;

    [Tooltip("Toggle to make Agent loop the Path")]
    public bool LoopPath; 
    
    private float _currentDistance = 0f;

    public override void Awake()
    {
        base.Awake();
        Target = new GameObject();
    }

    /// <summary>
    /// Generates a Sttering result based on Dynamic Pathfollowing behavior AI for Games by Ian Millington. Gets the next target before
    /// calling the base class, DynamicSeek.
    /// </summary>
    /// <returns>A Steering result.</returns>
    public override Steering GetSteering()
	{
        _currentDistance = Path.GetDistanceToPath(Agent.transform.position, _currentDistance);
        float targetDistance = _currentDistance + PathOffset;

		if (LoopPath && (Path.GetPosition(targetDistance) != Vector3.zero))
			Target.transform.position = Path.GetPosition(targetDistance);

		if (LoopPath == false && (Path.GetPosition(targetDistance) == Vector3.zero))
			return new Steering();

		else Target.transform.position = Path.GetPosition(targetDistance);

		return base.GetSteering();
    }
}
