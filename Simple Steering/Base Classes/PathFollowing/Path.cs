using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("Add Your GameObjects Here To Form Line Segments")]
    public List<GameObject> Points;

    private List<LineSegment> _segments;

	private void Start()
	{
		_segments = GetSegments();
	}

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

    public float GetDistanceToPath(Vector3 position, float lastCalculatedDistance)
	{
        float distance = 0f;
        float projectionPointDistance = 0f;
        LineSegment currentSegment = null;

        foreach (LineSegment pathSegment in _segments)
        {
            distance += Vector3.Distance(pathSegment.Start, pathSegment.End);
            if (lastCalculatedDistance <= distance)
            {
                currentSegment = pathSegment;
                break;
            }
        }

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

    public Vector3 GetPosition(float lastCalculatedDistance)
    {
        float distance = 0f;
        LineSegment currentSegment = null;

        foreach (LineSegment pathSegment in _segments)
        {
            distance += Vector3.Distance(pathSegment.Start, pathSegment.End);
            if (lastCalculatedDistance <= distance)
            {
                currentSegment = pathSegment;
                break;
            }

            if (pathSegment.Equals(_segments[_segments.Count - 1]) && this.transform.position.z == pathSegment.End.z)
			{
                Debug.Log($"Path Start: {pathSegment.Start} and Path End: {pathSegment.End}");
                return Vector3.zero;
			}
        }

		if (currentSegment == null)
			return Vector3.zero;

		Vector3 segmentDirection = GetNormalizedSegmentDirection(currentSegment.Start, currentSegment.End);
        distance -= Vector3.Distance(currentSegment.Start, currentSegment.End);
        distance = lastCalculatedDistance - distance;
        Vector3 position = currentSegment.Start + segmentDirection * distance;

        return position;
    }


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
        
        for (int i = 0; i < Points.Count - 1; i++)
        {
            Vector3 start = Points[i].transform.position;
            Vector3 end = Points[i + 1].transform.position;
            
            direction = end - start;
            Gizmos.DrawRay(start, direction);
        }

        Gizmos.color = color;
    }
}
