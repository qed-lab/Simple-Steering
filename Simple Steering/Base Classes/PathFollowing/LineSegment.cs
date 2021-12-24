using System.Collections;
using UnityEngine;

public class LineSegment
{
    public Vector3 Start;
    public Vector3 End;

    public LineSegment() : this(Vector3.zero, Vector3.zero) { }
    public LineSegment(Vector3 start, Vector3 end)
	{
        this.Start = start;
        this.End = end; 
	}

    public bool IsEmpty()
	{
        return true ? Start == Vector3.zero && End == Vector3.zero : false;
    }

    public override bool Equals(object obj)
	{
        LineSegment segment = (LineSegment)obj; 
        return true ? this.Start == segment.Start && this.End == segment.End : false;
	}
}
