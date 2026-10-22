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

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Slithering Snake");
            for (int i = 0; i < 5; i++) { Player.AddSegment(); }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(new Color(8, 64, 8));
            if (Input.IsKeyboardKeyPressed(KeyboardKey.Space)) { Player.AddSegment(); }
            if (Input.IsKeyboardKeyPressed(KeyboardKey.Tab)) { Player.Shadow = !Player.Shadow; }
            Player.Update();
        }
    }

}
