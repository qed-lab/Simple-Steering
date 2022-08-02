using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of line segments for a path for an Agent to follow utilizing a Steering Behavior.
/// </summary>
public class Path : MonoBehaviour
{
    /// <summary>
    /// The points that will make up the eventual path.
    /// </summary>
    [Header("Points on the Path")]
    public List<GameObject> Points;

    /// <summary>
    /// Determine if the path is closed or open. Links the first and last nodes together.
    /// </summary>
    //[Tooltip("Determine if the path is closed or open. Links the first and last nodes together.")]
    //public bool ClosePath = false;

    /// <summary>
    /// The segments that make up the path.
    /// </summary>
    private List<LineSegment> _segments;

	private void Start()
	{
		_segments = GetSegments();
	}

    /// <summary>
    /// Generate all segments along the path.
    /// </summary>
    /// <returns>A list of LineSegment objects.</returns>
    public List<LineSegment> GetSegments()
    {
        List<LineSegment> segments = new List<LineSegment>();

        for (int i = 0; i < Points.Count - 1; i++)
        {
            Vector3 start = Points[i].transform.position;
            Vector3 end = Points[i + 1].transform.position;

            segments.Add(new LineSegment(start, end));
        }

        return segments;
    }

    /// <summary>
    /// Get the closest (scalar) distance from a specific point to the total path.
    /// </summary>
    /// <param name="position">A position.</param>
    /// <param name="lastCalculatedDistance">The last distance calculated to the previous nearest node.</param>
    /// <returns>A scalar.</returns>
    public float GetDistanceToPath(Vector3 position, float lastCalculatedDistance)
	{
        float distance = 0f;
        float projectionPointDistance = 0f;
        LineSegment currentSegment = null;

        // Loop through every segment
        foreach (LineSegment pathSegment in _segments)
        {
            // Add the distance between the start and end points to the an accumulator
            distance += Vector3.Distance(pathSegment.Start, pathSegment.End);
            // If our lastCalculatedDistance is less than the current distance, we've found out next point/segment
            if (lastCalculatedDistance <= distance)
            {
                // Once found, set the currentSegment to the pathSegment we are evaluating.
                currentSegment = pathSegment;
                break;
            }
        }

        // If we didn't find one, return null (likely at the end of the segment
        if (currentSegment == null)
            return 0f;

        Vector3 currPosition = position - currentSegment.Start;
        Vector3 segmentDirection = GetNormalizedSegmentDirection(currentSegment.Start, currentSegment.End);

        // Scalar Projection
        Vector3 projectionPoint = Vector3.Project(currPosition, segmentDirection);
        projectionPointDistance = distance - Vector3.Distance(currentSegment.Start, currentSegment.End);
        projectionPointDistance += projectionPoint.magnitude;

        return projectionPointDistance;
    }

    /// <summary>
    /// Get the
    /// </summary>
    /// <param name="lastCalculatedDistance"></param>
    /// <returns></returns>
    public Vector3 GetPosition(float lastCalculatedDistance)
    {
        float distance = 0f;
        LineSegment currentSegment = null;

        // Loop through every segment
        foreach (LineSegment pathSegment in _segments)
        {
            // Add the distance between the start and end points to the an accumulator
            distance += Vector3.Distance(pathSegment.Start, pathSegment.End);
            // If our lastCalculatedDistance is less than the current distance, we've found out next point/segment
            if (lastCalculatedDistance <= distance)
            {
                currentSegment = pathSegment;
                break;
            }

            // If the pathSegment is equal to the last segment (in all coordinates), we've reached the end and bail entirely
            if (pathSegment.Equals(_segments[_segments.Count - 1]) && this.transform.position.z == pathSegment.End.z)
			{
                Debug.Log($"Path Start: {pathSegment.Start} and Path End: {pathSegment.End}");
                return Vector3.zero;
			}
        }

        // Bail if we haven't found a new segment to follow
		if (currentSegment == null)
			return Vector3.zero;

        // Generate the direction the agent needs to go for the next position
		Vector3 segmentDirection = GetNormalizedSegmentDirection(currentSegment.Start, currentSegment.End);
        distance -= Vector3.Distance(currentSegment.Start, currentSegment.End);
        distance = lastCalculatedDistance - distance;
        Vector3 position = currentSegment.Start + segmentDirection * distance;

        return position;
    }

    /// <summary>
    /// Get a normalized direction vector from a line segment.
    /// </summary>
    /// <param name="start">Line segment start.</param>
    /// <param name="end">Line segment end.</param>
    /// <returns></returns>
    private Vector3 GetNormalizedSegmentDirection(Vector3 start, Vector3 end)
	{
        Vector3 segmentDirection = end - start;
        return segmentDirection.normalized;
	}


    void OnDrawGizmos()
    {
        Vector3 direction;
        Color color = Gizmos.color;
        Gizmos.color = Color.green;
        
        // Guard in case Points isn't filled out by the time OnDrawGizmos is called
        if(Points != null)
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Vector3 start = Points[i].transform.position;
                Vector3 end = Points[i + 1].transform.position;
            
                direction = end - start;
                Gizmos.DrawRay(start, direction);
            }
        }

        Gizmos.color = color;
    }
}
