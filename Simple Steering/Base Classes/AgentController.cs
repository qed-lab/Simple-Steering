using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This is a base class that represents an AgentController for Steering Behaviors. 
/// </summary>
/// 

public class AgentController : MonoBehaviour
{
    // Create list of behaviours to add in inspector; Rename to AgentController 
    // Blending weights can add to more than 1; Arbitration adds up to just one; Blending can take a chance to use that behaviour 
    // Tuple<AgentBehaviour, int(weight)> weightedBehaviors = ... 

    public List<string> Behaviors = new List<string>();

    //public List<AgentBehaviour> agentBehaviours; 

    [System.NonSerialized]
    public Vector3 Velocity;
    [Header("Speed Parameters")]
    public float MaxSpeed;
    public float MaxAcceleration;

    [Header("Rotation Parameters")]
    public float MaxRotation;
    public float MaxAngularAcceleration;

    [System.NonSerialized]
    public float Orientation;
    [System.NonSerialized]
    public float Rotation;

    protected Steering Steering;

    private float scale = 0f;


    void Start()
    {
        Velocity = Vector3.zero;
        Steering = new Steering();
    }

    /// <summary>
    /// This represents a Kinematic Update 
    /// </summary>
    public virtual void Update()
    {
        Vector3 displacement = Velocity * Time.deltaTime;
        Orientation += Rotation * Time.deltaTime;

        // Limit the Orientation values to be in the range (0 - 360)
        if (Orientation < 0.0f)
            Orientation += 360.0f;
        else if (Orientation > 360.0f)
            Orientation -= 360.0f;
        
        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, Orientation);
    }

    /// <summary>
    /// This represents a Dynamic Update 
    /// </summary>
    public virtual void LateUpdate()
    {
        if (Steering is null)
            return; 

        Velocity += Steering.Linear * Time.deltaTime;
        Rotation += Steering.Angular * Time.deltaTime;
        if (Velocity.magnitude > MaxSpeed)
        {
            Velocity.Normalize();
            Velocity *= MaxSpeed;
        }

        if (Steering.Angular == 0.0f)
            Rotation = 0.0f;

        if (Steering.Linear.sqrMagnitude == 0.0f)
            Velocity = Vector3.zero;
        
        Steering = new Steering();
    }

    /// <summary>
    /// Sets Steering behavior for derived classes.
    /// </summary>
    /// <param name="result"></param>
    public void SetSteering(Steering result)
    {
        this.Steering = result;
    }
}