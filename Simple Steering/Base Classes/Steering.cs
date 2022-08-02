using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Steering Output for Dynamic and Kinematic Agents.
/// </summary>
public class Steering
{
    /// <summary>
    /// The rotational velocity of the agent around the y (3D)/z (2D) axis.
    /// </summary>
    public float Angular;
    
    /// <summary>
    /// The directional velocity of the agent in all directions (x, y, z).
    /// </summary>
    public Vector3 Linear;

    public Steering()
    {
        Angular = 0.0f;
        Linear = new Vector3();
    }
}