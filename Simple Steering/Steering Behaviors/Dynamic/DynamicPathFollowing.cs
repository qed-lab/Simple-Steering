using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Path Following Steering behavior for agents.
/// </summary>
public class DynamicPathFollowing : DynamicSeek
{
    [Header("Insert Path (Script) Here (Drag & Drop from Inspector, not Assets folder)")]
    public Path Path;

    [Header("Higher Offset = Smoother Steering\n(e.g values 100-1000; If higher, make sure to space out path; This also affects the distance to the endpoint)")]
    [Range(100, 4000)]
    public float PathOffset = 0f;

    [Header("Toggle to make Agent loop in Path")]
    public bool LoopInPath; 
    
    private float _currentDistance = 0f;

    public override void Awake()
    {
        base.Awake();
        Target = new GameObject();
    }

    public override Steering GetSteering()
	{
        _currentDistance = Path.GetDistanceToPath(Agent.transform.position, _currentDistance);
        float targetDistance = _currentDistance + PathOffset;

		if (LoopInPath && (Path.GetPosition(targetDistance) != Vector3.zero))
			Target.transform.position = Path.GetPosition(targetDistance);

		if (LoopInPath == false && (Path.GetPosition(targetDistance) == Vector3.zero))
			return new Steering();

		else Target.transform.position = Path.GetPosition(targetDistance);


		return base.GetSteering();
    }
}
