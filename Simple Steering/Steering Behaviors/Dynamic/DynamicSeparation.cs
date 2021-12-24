using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Separation Steering behavior for agents.
/// </summary>
public class DynamicSeparation : AgentBehaviour
{
    // A list of potential _targets.
    private GameObject[] _targets;

    // The Threshold to take action
    public float Threshold;

    // The constant coefficient of decay for the inverse square law.
    public float DecayCoefficient;

    private void Start()
    {
        // Initialize _targets list; Grab all GameObjects with 'Target' Tag
        _targets = GameObject.FindGameObjectsWithTag("Target");
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        foreach (GameObject target in _targets)
        {
            Vector3 direction = target.transform.position - Agent.transform.position;

            float distance = direction.magnitude;
            float strength;
            if (distance < Threshold)
            {
                // Calculate the strength of repulsion (here using Linear Separation).
                strength = Agent.MaxAcceleration * (Threshold - distance) / Threshold;
                Debug.Log($"Strength: {strength}");

                // Add the acceleration
                direction.Normalize();
                steering.Linear += strength * direction; 
            }
        }

        return steering;
    }
}
