using System;
using System.Numerics;

namespace MohawkGame2D;

public struct BodyPart(Vector2 position, Vector2 direction)
{
    public Vector2 Position { get; set; } = position;
    public Vector2 Direction { get; set; } = direction;
    public float DesiredDistance { get; } = 25f;

    public void ConstrainDistance(Vector2 anchor)
    {
        if ((Position - anchor).Length() != DesiredDistance)
        {
            Direction = Vector2.Normalize(anchor - Position);
            Position = anchor - Direction * DesiredDistance;
        }
    }
}
