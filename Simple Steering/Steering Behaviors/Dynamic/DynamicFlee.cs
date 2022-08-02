/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Dynamic Flee Steering behavior for agents.
/// </summary>
public class DynamicFlee : AgentBehaviour
{
    /// <summary>
    /// Generates a Steering behavior according to the rules of Dynamic Flee as defined by AI for Games by Ian Millington.
    /// Will attempt to get away from a specific target and stay as far away.
    /// </summary>
    /// <returns>A Steering object.</returns>
    public override Steering GetSteering()
    {
        Steering result = new Steering();
        
        result.Linear = transform.position - Target.transform.position;
        LookWhereYoureGoing(result.Linear);
        result.Linear.Normalize();
        result.Linear *= Agent.MaxAcceleration;

        return result;
    }
}