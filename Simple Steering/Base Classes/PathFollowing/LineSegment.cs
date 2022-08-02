using UnityEngine;

/// <summary>
/// Two points that make up the segment of the total path. The start and end points are represented
/// in the world's coordinates, not an abstraction of the world.
/// </summary>
public class LineSegment
{
    /// <summary>
    /// The starting point of a line segment in 3D space in Unity.
    /// </summary>
    public Vector3 Start;

    /// <summary>
    /// The ending point of a line segment in 3D space in Unity.
    /// </summary>
    public Vector3 End;

    /// <summary>
    /// Empty constructor, initializes the line segment start and end points to (0,0,0) in Unity space.
    /// </summary>
    public LineSegment() : this(Vector3.zero, Vector3.zero) { }

    /// <summary>
    /// Initializes a line segment to two provided points, the start and end.
    /// </summary>
    /// <param name="start">Starting point in 3D space.</param>
    /// <param name="end">Ending point in 3D space.</param>
    public LineSegment(Vector3 start, Vector3 end)
	{
        this.Start = start;
        this.End = end; 
	}

    /// <summary>
    /// Returns if the line has a length of zero.
    /// </summary>
    /// <returns>True if both the start and end are zero, false otherwise.</returns>
    public bool IsEmpty()
	{
        return true ? Start == Vector3.zero && End == Vector3.zero : false;
    }

    /// <summary>
    /// Check if two (LineSegment) objects are the same.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if the start & end of the two segments are the same, false otheriwse.</returns>
    public override bool Equals(object obj)
	{
        LineSegment segment = (LineSegment)obj;
        
        // Check if segment isn't null first to prevent potential crashes because object isn't a LineSegment
        return true ? segment != null &&
                        this.Start == segment.Start &&
                        this.End == segment.End : false;
	}
}
