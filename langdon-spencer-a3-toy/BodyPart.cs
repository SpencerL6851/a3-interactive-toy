using System;
using System.Numerics;

namespace MohawkGame2D;

public struct BodyPart(Vector2 position, Vector2 direction)
{
    public Vector2 Position { get; set; } = position;
    public Vector2 Direction { get; set; } = direction;
    public float DesiredDistance { get; } = 17.5f;

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
