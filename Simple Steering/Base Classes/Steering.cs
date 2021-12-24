using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Steering Output for Dynamic and Kinematic Agents.
/// </summary>
public class Steering
{
    public float Angular;
    public Vector3 Linear;
    public Steering()
    {
        Angular = 0.0f;
        Linear = new Vector3();
    }
}