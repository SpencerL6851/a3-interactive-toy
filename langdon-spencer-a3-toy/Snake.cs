using System;
using System.Numerics;

namespace MohawkGame2D;

public class Snake()
{
    public BodyPart[] BodyArray { get; set; } = [new(new Vector2(400f, 300f), new Vector2(1f, 0f)), new(new Vector2(390f, 300f), new Vector2(1f, 0f))];
    public void AddSegment()
    {
        BodyPart[] _ = new BodyPart[BodyArray.Length + 1];
        Array.Copy(BodyArray, _, BodyArray.Length);
        _[^1] = new(BodyArray[^1].Position - BodyArray[^1].Direction * BodyArray[^1].DesiredDistance, BodyArray[^1].Direction);
        BodyArray = _;
    }
    public void Update()
    {
        float angle = 0f;
        if (Input.IsKeyboardKeyDown(KeyboardKey.Left))
        {
            angle -= 6f * Time.DeltaTime;
        }
        if (Input.IsKeyboardKeyDown(KeyboardKey.Right))
        {
            angle += 6f * Time.DeltaTime;
        }
        BodyArray[0].Direction = new Vector2(BodyArray[0].Direction.X * (float)Math.Cos(angle) - BodyArray[0].Direction.Y * (float)Math.Sin(angle), BodyArray[0].Direction.X * (float)Math.Sin(angle) + BodyArray[0].Direction.Y * (float)Math.Cos(angle));
        BodyArray[0].Position += BodyArray[0].Direction * 225 * Time.DeltaTime;
        for (int i = 0; i < BodyArray.Length; i++) 
        {
            if (i != 0)
            {
                BodyArray[i].ConstrainDistance(BodyArray[i - 1].Position); 
            }
        }
        for (int i = BodyArray.Length - 1; i >= 0; i--)
        {
            if (i % 2 == 0) { Draw.FillColor = Color.Gray; } else { Draw.FillColor = Color.DarkGray; }
            Draw.Circle(BodyArray[i].Position, BodyArray[0].DesiredDistance - (i * (BodyArray[0].DesiredDistance / 2) / BodyArray.Length));
            if (i == 0)
            {
                Draw.FillColor = Color.Black;
                Draw.Circle(BodyArray[0].Position + new Vector2(-BodyArray[0].Direction.Y, BodyArray[0].Direction.X) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
                Draw.Circle(BodyArray[0].Position - new Vector2(-BodyArray[0].Direction.Y, BodyArray[0].Direction.X) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
            }
        }
    }
}
