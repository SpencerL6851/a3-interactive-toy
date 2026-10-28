// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        Snake Player = new();
        public float foodSpawnTimer = 0f;
        public Food[] food = new Food[5];
        private readonly bool debug = false;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.ClearBackground(new Color(24, 96, 24));
            Window.SetTitle("Slithering Snake");
            for (int i = 0; i < 5; i++) { Player.AddSegment(); }
            for (int i = 0; i < food.Length; i++)
            {
                food[i] = new Food();
                food[i].RandomizePosition(Player); 
            }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            // Spawning food every 2.5s if there are less than 5 food spawned.
            if (!Player.Paused)
            {
                foodSpawnTimer += Time.DeltaTime;
                if (foodSpawnTimer >= 2.5f)
                {
                    for (int i = 0; i < food.Length; i++)
                    {
                        if (!food[i].Spawned)
                        {
                            food[i].Spawned = true;
                            food[i].RandomizePosition(Player);
                            break;
                        }

                    }
                    foodSpawnTimer = 0;
                }
            }
            Window.ClearBackground(new Color(24, 96, 24));
            if (Input.IsKeyboardKeyPressed(KeyboardKey.Space)) { Player.AddSegment(); }
            
            if (Input.IsKeyboardKeyPressed(KeyboardKey.Tab) & debug) { Player.Shadow = !Player.Shadow; }
            
            if (Input.IsKeyboardKeyPressed(KeyboardKey.P)) { Player.Paused = !Player.Paused; }
            Player.Update();
            for (int i = 0; i < food.Length; i++)
            {
                food[i].Update(Player);
            }
            if (Player.Paused)
            {
                Draw.FillColor = new ColorF(0f, 0f, 0f, 0.4f);
                Draw.Rectangle(new Vector2(0, 0), Window.Size);
            }
        }
    }

}
