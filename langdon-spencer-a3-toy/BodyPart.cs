using System;
using System.Numerics;

namespace MohawkGame2D;

public struct BodyPart(Vector2 position, bool isHead)
{
    public Vector2 Position { get; set; } = position;
    public float Distance { get; } = 20.0f;
    public bool IsHead { get; } = isHead;

    public void Constrain(Vector2 anchor)
    {
        if ((Position - anchor).Length() != Distance & !IsHead)
        {
            Position = Vector2.Normalize(Position - anchor) * Distance;
        }
    }
}
