using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace MohawkGame2D;

public class Snake() 
{
    public bool Shadow { get; set; }
    public BodyPart[] BodyArray { get; set; } = [new(new Vector2(400f, 300f), new Vector2(1f, 0f)), new(new Vector2(390f, 300f), new Vector2(1f, 0f))];
    
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
    /// Adds a segment to the snake.
    /// </summary>
    public void AddSegment()
    {
        BodyPart[] _ = new BodyPart[BodyArray.Length + 1];
        Array.Copy(BodyArray, _, BodyArray.Length);
        _[^1] = new(BodyArray[^1].Position - BodyArray[^1].Direction * BodyArray[^1].DesiredDistance, BodyArray[^1].Direction);
        BodyArray = _;
    }

    /// <summary>
    /// Draws points describing the outline of the snake, with extra points on the head and tail. (DEBUG)
    /// </summary>
    public void ShowBodyShape()
    {
        Draw.LineColor = Draw.FillColor = new(255, 64, 64);
        for (int i = 0; i < BodyArray.Length; i++)
        {
            float radius = BodyArray[0].DesiredDistance - (i * (BodyArray[0].DesiredDistance / 2) / BodyArray.Length);
            Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, 90, true), 2);
            Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, -90, true), 2);
            if (i == 0)
            {
                Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, 45, true), 2);
                Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, -45, true), 2);
                Draw.Circle(BodyArray[i].Position + BodyArray[i].Direction * radius, 2);
            }
            if (i == BodyArray.Length - 1)
            {
                Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, 135, true), 2);
                Draw.Circle(BodyArray[i].Position + RotateVector(BodyArray[i].Direction * radius, -135, true), 2);
                Draw.Circle(BodyArray[i].Position - BodyArray[i].Direction * radius, 2);
            }
        }
        Draw.LineColor = Draw.FillColor = Color.Black;
    }

    /// <summary>
    /// Draws the spine of the snake. (DEBUG)
    /// </summary>
    public void ShowSpine()
    {
        Draw.LineColor = Draw.FillColor = new(64, 64, 255);
        for (int i = 0; i < BodyArray.Length; i++)
        {
            if (i != 0)
            {
                Draw.Line(BodyArray[i].Position, BodyArray[i - 1].Position);
            }
            Draw.FillColor = new(64, 64, 255);
            Draw.Circle(BodyArray[i].Position, 2);
        }
        Draw.LineColor = Draw.FillColor = Color.Black;
    }

    /// <summary>
    /// This should be called every frame.
    /// </summary>
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
        BodyArray[0].Direction = RotateVector(BodyArray[0].Direction, angle, false);
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
            Draw.LineColor = Color.Black;
            if (!Shadow)
            {
                if (i % 2 == 0)
                {
                    Draw.FillColor = new(96, 64, 128);
                    Draw.LineColor = new(48, 32, 64);
                }
                else
                {
                    Draw.FillColor = new(128, 96, 196);
                    Draw.LineColor = new(64, 48, 98);
                }
            }
            else
            {
                Draw.LineColor = Draw.FillColor = Color.Black;
            }
            Draw.Circle(BodyArray[i].Position, BodyArray[0].DesiredDistance - (i * (BodyArray[0].DesiredDistance / 2) / BodyArray.Length));
            if (i == 0)
            {
                Draw.LineColor = Draw.FillColor = Color.Black;
                Draw.Circle(BodyArray[0].Position + RotateVector(BodyArray[0].Direction, 50, true) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
                Draw.Circle(BodyArray[0].Position + RotateVector(BodyArray[0].Direction, -50, true) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
            }
        }
        if (Shadow)
        {
            ShowSpine();
            ShowBodyShape();
        }
    }
}
