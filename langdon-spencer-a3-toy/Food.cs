using System;
using System.Numerics;

namespace MohawkGame2D;
public class Food
{
    public Vector2 Position;
    public bool Spawned = false;
    private readonly float SpawningDeadzone = 100f;

    /// <summary>
    /// Randomizes the position of the goal to a position that is not near the snake or the edges of the screen.
    /// </summary>
    public void RandomizePosition(Snake player)
    {
        Position = Random.Vector2(Window.Size);
        bool validPosition = true;
        for (int i = 0; i < player.BodyArray.Length; i++) 
        {
            if (Vector2.Distance(Position, player.BodyArray[i].Position) < 2 * (player.BodyArray[0].DesiredDistance - (i * (player.BodyArray[0].DesiredDistance / 2) / player.BodyArray.Length)))
            {
                validPosition = false; break;
            }
            if (Position.X < SpawningDeadzone | Position.X > Window.Width - SpawningDeadzone | Position.Y < SpawningDeadzone | Position.Y > Window.Height - SpawningDeadzone)
            {
                validPosition = false; break;
            }
        }
        while (!validPosition)
        {
            Position = Random.Vector2(Window.Size);
            validPosition = true;
            for (int i = 0; i < player.BodyArray.Length; i++)
            {
                if (Vector2.Distance(Position, player.BodyArray[i].Position) < 2 * player.BodyArray[0].DesiredDistance - (i * (player.BodyArray[0].DesiredDistance / 2) / player.BodyArray.Length))
                {
                    validPosition = false; break;
                }
                if (Position.X < SpawningDeadzone | Position.X > Window.Width - SpawningDeadzone | Position.Y < SpawningDeadzone | Position.Y > Window.Height - SpawningDeadzone)
                {
                    validPosition = false; break;
                }
            }
        }
    }
    /// <summary>
    /// This should be run every unpaused frame.
    /// </summary>
    public void Update(Snake player)
    {
        if (Spawned)
        {
            Draw.FillColor = Color.Yellow;
            Draw.Circle(Position, player.BodyArray[0].DesiredDistance / 1.5f);
            if (Vector2.Distance(Position, player.BodyArray[0].Position) < 2 * player.BodyArray[0].DesiredDistance)
            {
                player.AddSegment();
                Spawned = false;
            }
        }
    }
}
