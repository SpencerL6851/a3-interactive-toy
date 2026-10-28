using System;
using System.Numerics;

namespace MohawkGame2D;

public class Snake() 
{
    public bool Shadow { get; set; }
    public bool Paused { get; set; }
    private ColorF Color1 { get; } = new(0f, 0.75f, 1f);
    private ColorF Color2 { get; } = new(0.5f, 0.75f, 1f);
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
    public void ShowBodyOutline()
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
        Draw.LineSize = 1;
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
    /// This should be called every frame that is unpaused.
    /// </summary>
    public void Update()
    {
        float deltaR = (Color2.R - Color1.R) / BodyArray.Length;
        float deltaG = (Color2.G - Color1.G) / BodyArray.Length;
        float deltaB = (Color2.B - Color1.B) / BodyArray.Length;
        float angle = 0f;
        if (!Paused)
        {
            if (Input.IsKeyboardKeyDown(KeyboardKey.Left))
            {
                angle -= 5f * Time.DeltaTime;
            }

            if (Input.IsKeyboardKeyDown(KeyboardKey.Right))
            {
                angle += 5f * Time.DeltaTime;
            }
            BodyArray[0].Direction = RotateVector(BodyArray[0].Direction, angle, false);
            BodyArray[0].Position += BodyArray[0].Direction * 180 * Time.DeltaTime;
        }
            
        for (int i = 1; i < BodyArray.Length; i++)
        {
            BodyArray[i].ConstrainDistance(BodyArray[i - 1].Position);
        }

        for (int i = 3; i < BodyArray.Length; i++)
        {
            if (Vector2.Distance(BodyArray[0].Position, BodyArray[i].Position) < (1.85 * BodyArray[0].DesiredDistance) - (i * (BodyArray[0].DesiredDistance / 2) / BodyArray.Length))
            {
                throw new Exception("Crashing program on purpose.");
            }
        }
        if (BodyArray[0].Position.X < BodyArray[0].DesiredDistance | BodyArray[0].Position.X > Window.Width - BodyArray[0].DesiredDistance |
            BodyArray[0].Position.Y < BodyArray[0].DesiredDistance | BodyArray[0].Position.Y > Window.Height - BodyArray[0].DesiredDistance)
        {
            throw new Exception("Crashing program on purpose.");
        }

        for (int i = 0; i < BodyArray.Length; i++)
        {
            float radius = BodyArray[0].DesiredDistance - (i * (BodyArray[0].DesiredDistance / 2) / BodyArray.Length);
            Draw.LineSize = 0;
            if (Shadow)
            {
                Draw.FillColor = Color.Black;
            }
            else
            {
                Draw.FillColor = new ColorF(Color1.R + deltaR * i, Color1.G + deltaG * i, Color1.B + deltaB * i);
            }
            
            if (i == 0)
            {
                Draw.Quad(
                    BodyArray[0].Position + RotateVector(BodyArray[0].Direction, 90, true) * radius,
                    BodyArray[0].Position + RotateVector(BodyArray[0].Direction, 45, true) * radius,
                    BodyArray[0].Position + BodyArray[0].Direction * radius,
                    BodyArray[0].Position
                    );
                Draw.Quad(
                    BodyArray[0].Position + RotateVector(BodyArray[0].Direction, -90, true) * radius,
                    BodyArray[0].Position + RotateVector(BodyArray[0].Direction, -45, true) * radius,
                    BodyArray[0].Position + BodyArray[0].Direction * radius,
                    BodyArray[0].Position
                    );
                if (Shadow) {  Draw.FillColor = Color.White; } 
                else { Draw.FillColor = Color.Black; }
                Draw.Circle(BodyArray[0].Position + RotateVector(BodyArray[0].Direction, 50, true) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
                Draw.Circle(BodyArray[0].Position + RotateVector(BodyArray[0].Direction, -50, true) * (BodyArray[0].DesiredDistance * 0.7f), (BodyArray[0].DesiredDistance * 0.2f));
            }
            
            else
            {
                Draw.Quad(
                    BodyArray[i].Position + RotateVector(BodyArray[i].Direction, 90, true) * radius,
                    BodyArray[i - 1].Position + RotateVector(BodyArray[i - 1].Direction, 90, true) * radius,
                    BodyArray[i - 1].Position + RotateVector(BodyArray[i - 1].Direction, -90, true) * radius,
                    BodyArray[i].Position + RotateVector(BodyArray[i].Direction, -90, true) * radius
                );
                if (i == BodyArray.Length - 1)
                {
                    Draw.LineSize = 0;
                    Draw.Quad(
                        BodyArray[i].Position - RotateVector(BodyArray[i].Direction, 90, true) * radius,
                        BodyArray[i].Position - RotateVector(BodyArray[i].Direction, 45, true) * radius,
                        BodyArray[i].Position - BodyArray[i].Direction * radius,
                        BodyArray[i].Position
                        );
                    Draw.Quad(
                        BodyArray[i].Position - RotateVector(BodyArray[i].Direction, -90, true) * radius,
                        BodyArray[i].Position - RotateVector(BodyArray[i].Direction, -45, true) * radius,
                        BodyArray[i].Position - BodyArray[i].Direction * radius,
                        BodyArray[i].Position
                        );
                }
            }
        }
        if (Shadow)
        {
            ShowSpine();
            ShowBodyOutline();
        }
    }
}
