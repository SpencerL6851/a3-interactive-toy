using System;
using System.Numerics;

namespace MohawkGame2D;

public struct BodyPart(Vector2 position, Vector2 direction)
{
    public Vector2 Position { get; set; } = position;
    public Vector2 Direction { get; set; } = direction;
    public float DesiredDistance { get; } = 17.5f;
    public float DesiredAngle { get; } = 10f;

    /// <summary>
    /// Rotates a vector by a given angle.
    /// </summary>
    /// <param name="degrees"> If true, specified angle is converted to radians before calculation</param>
    /// <returns>The rotated vector.</returns>
    public Vector2 RotateVector(Vector2 vector, float angle, bool degrees)
    {
        float newAngle = angle;
        if (degrees)
        {
            newAngle = (float)(angle * Math.PI / 180);
        }
        return new Vector2(vector.X * (float)Math.Cos(newAngle) - vector.Y * (float)Math.Sin(newAngle), vector.X * (float)Math.Sin(newAngle) + vector.Y * (float)Math.Cos(newAngle));
    }
    /// <summary>
    /// Gives the change in direction between two body segments.
    /// </summary>
    public float GetBodyBend(Vector2 anchorDirection)
    {
        return (float)Math.Atan2(Vector2.Cross(anchorDirection, Direction), Vector2.Dot(anchorDirection, Direction));
    }

    public void ConstrainAngle(Vector2 anchorDirection)
    {
        if (Math.Abs((float)(GetBodyBend(anchorDirection) * 180 / Math.PI)) > DesiredAngle)
        {
            if ((float)(GetBodyBend(anchorDirection) * 180 / Math.PI) > 0)
            {
                Direction = RotateVector(anchorDirection, (float)Math.CopySign(DesiredAngle, GetBodyBend(anchorDirection)), false);
            }
            else
            {
                Direction = RotateVector(anchorDirection, (float)Math.CopySign(DesiredAngle, -GetBodyBend(anchorDirection)), false);
            }
        }
    }

    /// <summary>
    /// Constrains the distance to the anchor point and automatically updates the 'Position' and 'Direction' properties.
    /// </summary>
    /// <param name="anchor">The anchor point.</param>
    public void ConstrainDistance(Vector2 anchor)
    {
        if ((Position - anchor).Length() != DesiredDistance)
        {
            Direction = Vector2.Normalize(anchor - Position);
            Position = anchor - Direction * DesiredDistance;
        }
        
    }
    
}
