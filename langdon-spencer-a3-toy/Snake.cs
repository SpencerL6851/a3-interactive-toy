using System;
using System.Numerics;

namespace MohawkGame2D;

public class Snake()
{
    public BodyPart[] BodyArray { get; set; } = [new(new Vector2(400f, 300f), new Vector2(1f, 0f), 1, true)];
    public void AddSegment()
    {
        BodyPart[] _ = new BodyPart[BodyArray.Length + 1];
        BodyArray.CopyTo(_, 0);
        _[^1] = new(_[^2].Position - _[^2].Direction * _[^2].Distance, _[^2].Direction, BodyArray.Length, false);
        BodyArray = _;
    }
    public void Update()
    {
        float angle = 0f;
        if (Input.IsKeyboardKeyDown(KeyboardKey.Left))
        {
            angle -= 5f;
        }
        if (Input.IsKeyboardKeyDown(KeyboardKey.Right))
        {
            angle += 5f;
        }
        BodyArray[0].Direction = new Vector2(BodyArray[0].Direction.X * (float)Math.Cos(angle) - BodyArray[0].Direction.Y * (float)Math.Sin(angle), BodyArray[0].Direction.X * (float)Math.Sin(angle) + BodyArray[0].Direction.Y * (float)Math.Cos(angle));
        BodyArray[0].Position += BodyArray[0].Direction * 5;
        for (int i = 1; i < BodyArray.Length; i++) 
        {
            BodyArray[i].ConstrainDistance(BodyArray[i - 1].Position);
        }
    }
}
