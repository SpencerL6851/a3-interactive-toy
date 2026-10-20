using System;
using System.Numerics;

namespace MohawkGame2D;

public struct BodyPart(Vector2 position, Vector2 direction, int order, bool isHead)
{
    public Vector2 Position { get; set; } = position;
    public Vector2 Direction { get; set; } = direction;
    public int Order { get; } = order;
    public float Distance { get; } = 10f;
    public bool IsHead { get; } = isHead;

    public void ConstrainDistance(Vector2 anchor)
    {
        if ((Position - anchor).Length() != Distance & !IsHead)
        {

            Direction = Vector2.Normalize(anchor - Position);
            Position = -Direction * Distance;
        }
    }
}
